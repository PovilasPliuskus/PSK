import { Dispatch, FormEvent, SetStateAction, useState, DragEvent } from "react";
import { StatusEnum } from "../../../Models/TaskEnums";
import { TaskSummary } from "../../../Models/TaskSummary";
import { motion } from "framer-motion";
import { FiPlus } from "react-icons/fi";
import { axiosInstance } from "../../../utils/axiosInstance";
import DropIndicator from "./DropIndicator";
import './TaskBoardViewPage.css';
import Card from "./Card";
import { UpdateTaskBody } from "../../../Models/RequestBodies/UpdateTaskBody";
import ScriptResources from "../../../../src/assets/resources/strings"
import { CreateTaskBody } from "../../../Models/RequestBodies/CreateTaskBody";

type ColumnProps = {
  title: string;
  headingColor: string;
  cards: TaskSummary[];
  column: StatusEnum;
  setCards: Dispatch<SetStateAction<TaskSummary[]>>;
  fetchTasks: () => void;
  workspaceId: string;
};

const Column: React.FC<ColumnProps> = ({
  title,
  headingColor,
  cards,
  column,
  setCards,
  fetchTasks,
  workspaceId
}) => {
  const [active, setActive] = useState(false);

  const handleDragStart = (e: DragEvent, card: TaskSummary) => {
    e.dataTransfer && e.dataTransfer.setData("cardId", card.id);
  };

  const changeCardStatusInBackend = (card: TaskSummary) => {
    const cardPutRequest = (taskSummary: TaskSummary) => {
      const updateTaskBody: UpdateTaskBody = {
        id: taskSummary.id,
        workspaceId: taskSummary.workspaceId,
        name: taskSummary.name,
        createdByUserEmail: taskSummary.createdByUserEmail,
        dueDate: taskSummary.dueDate ? new Date(taskSummary.dueDate) : null,
        assignedToUserEmail: taskSummary.assignedToUserEmail,
        status: taskSummary.status,
        estimate: taskSummary.estimate,
        type: taskSummary.type,
        priority: taskSummary.priority,
        version: taskSummary.version,
        force: false,
      }

      axiosInstance.put(`/task`, updateTaskBody)
        .then(response => {
          console.log(response);
          const returnedCard = response.data;
          // TODO update cards once response is received.
        })
        .catch(error => {
          if (error.status === 409) {
            const userChoice = window.confirm(ScriptResources.OptimisticLockingUserChoice);
            if (userChoice) {
              updateTaskBody.force = true;
              axiosInstance.put(`/task`, updateTaskBody)
                .catch(error => {
                  console.error("Error forcing task update:", error);
                })
            }
            else {
              fetchTasks();
            }
          }
          else {
            console.error("Error updating task:", error);
            console.log(error);
          }
        })
    }

    if (card != null) {
      cardPutRequest(card);
    }
  }

  const handleDragEnd = (e: DragEvent) => {
    const cardId = e.dataTransfer && e.dataTransfer.getData("cardId");

    setActive(false);
    clearHighlights();

    const indicators = getIndicators();
    const { element } = getNearestIndicator(e, indicators);

    const before = element.dataset.before || "-1";

    if (before !== cardId) {
      let copy = [...cards];

      let cardToTransfer = copy.find((c) => c.id === cardId);
      if (!cardToTransfer) return;
      cardToTransfer = { ...cardToTransfer, status: column };

      changeCardStatusInBackend(cardToTransfer);

      copy = copy.filter((c) => c.id !== cardId);
      const moveToBack = before === "-1";

      if (moveToBack) {
        copy.push(cardToTransfer);
      } else {
        const insertAtIndex = copy.findIndex((el) => el.id === before);
        if (insertAtIndex === undefined) return;

        copy.splice(insertAtIndex, 0, cardToTransfer);
      }

      setCards(copy);
    }
  };

  const handleDragOver = (e: DragEvent) => {
    e.preventDefault();
    highlightIndicator(e);

    setActive(true);
  };

  const clearHighlights = (els?: HTMLElement[]) => {
    const indicators = els || getIndicators();

    indicators.forEach((i) => {
      i.style.opacity = "0";
    });
  };

  const highlightIndicator = (e: DragEvent) => {
    const indicators = getIndicators();

    clearHighlights(indicators);

    const el = getNearestIndicator(e, indicators);

    el.element.style.opacity = "1";
  };

  const getNearestIndicator = (e: DragEvent, indicators: HTMLElement[]) => {
    const DISTANCE_OFFSET = 50;

    const el = indicators.reduce(
      (closest, child) => {
        const box = child.getBoundingClientRect();

        const offset = e.clientY - (box.top + DISTANCE_OFFSET);

        if (offset < 0 && offset > closest.offset) {
          return { offset: offset, element: child };
        } else {
          return closest;
        }
      },
      {
        offset: Number.NEGATIVE_INFINITY,
        element: indicators[indicators.length - 1],
      }
    );

    return el;
  };

  const getIndicators = () => {
    return Array.from(
      document.querySelectorAll(
        `[data-column="${column}"]`
      ) as unknown as HTMLElement[]
    );
  };

  const handleDragLeave = () => {
    clearHighlights();
    setActive(false);
  };


  const filteredCards = cards.filter((c) => c.status === column);

  return (
    <div className="column">
      <div className="column-header">
        <h3 className={`column-title ${headingColor}`}>{title}</h3>
        <span className="column-count">
          {filteredCards.length}
        </span>
      </div>
      <div
        onDrop={handleDragEnd}
        onDragOver={handleDragOver}
        onDragLeave={handleDragLeave}
        className={`column-content ${active ? "column-content-active" : ""}`}
      >
        {filteredCards.map((c) => {
          return <Card key={c.id} task={c} handleDragStart={handleDragStart} />;
        })}
        <DropIndicator beforeId={null} column={column} />
        <AddCard column={column} setCards={setCards} workspaceId={workspaceId} fetchTasks={fetchTasks}/>
      </div>
    </div>
  );
};

type AddCardProps = {
  column: StatusEnum;
  setCards: Dispatch<SetStateAction<TaskSummary[]>>;
  workspaceId: string;
  fetchTasks: () => void;
};

const AddCard = ({ column, setCards, workspaceId, fetchTasks }: AddCardProps) => {
  const [name, setName] = useState("");
  const [adding, setAdding] = useState(false);

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!name.trim().length) return;

    const createTaskBody : CreateTaskBody = {
      name: name.trim(),
      workspaceId: workspaceId,
      status: column,
      version: 0,
    }

    const cardCreationRequest = () => {
      axiosInstance.post(`/task`, createTaskBody)
        .then(response => {
          fetchTasks();
          //const returnedCard = response.data;
          //setCards((pv) => [...pv, returnedCard]);
        })
        .catch(error => {
          console.error(error);
        })
    }


    cardCreationRequest();

    setAdding(false);
    setName("");
  };

  return (
    <>
      {adding ? (
        <motion.form layout onSubmit={handleSubmit} className="add-card-form">
          <textarea
            onChange={(e) => setName(e.target.value)}
            autoFocus
            placeholder="Add new task name..."
            className="add-card-textarea"
            value={name}
          />
          <div className="add-card-actions">
            <button
              onClick={() => setAdding(false)}
              className="close-button"
            >
              Close
            </button>
            <button
              type="submit"
              className="add-button"
            >
              <span>Add</span>
              <FiPlus />
            </button>
          </div>
        </motion.form>
      ) : (
        <motion.button
          layout
          onClick={() => setAdding(true)}
          className="add-card-button"
        >
          <span>Add card</span>
          <FiPlus />
        </motion.button>
      )}
    </>
  );
};

export default Column;