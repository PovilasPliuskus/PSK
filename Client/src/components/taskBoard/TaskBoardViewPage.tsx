import React, {
  Dispatch,
  SetStateAction,
  useState,
  DragEvent,
  FormEvent,
  useEffect,
} from "react";
import { FiPlus, FiTrash } from "react-icons/fi";
import { motion } from "framer-motion";
import { FaFire } from "react-icons/fa";
import { BsHammer } from "react-icons/bs";
import './TaskBoardViewPage.css';
import { Button, Modal, Form } from "react-bootstrap";
import { estimateMapper, priorityMapper, statusMapper, typeMapper } from "../utils/enumMapper.tsx";
import { CardType, StatusEnum } from "../utils/types.ts";
import { axiosInstance } from '../../utils/axiosInstance';
import { useParams } from "react-router-dom";
import keycloak from '../../keycloak';
import SomethingWentWrong from "../base/SomethingWentWrong.tsx";
import Loading from "../base/Loading.tsx";
import ScriptResources from "../../assets/resources/strings.ts";

export const TaskBoardViewPage = () => {
  const { id } = useParams();
  const [cards, setCards] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  
  useEffect(() => {
    const fetchTasks = () => {
      setError(null);
      setIsLoading(true);
      axiosInstance.get(`/task/${id}`, {
        params: { pageNumber: 1, pageSize: 10 },
      })
      .then(response => {
        setCards(response.data);
      })
      .catch(error => {
        setError(error);
        console.log(error);
      })
      .finally(() => {
        setIsLoading(false);
      })
    }
    if (keycloak.authenticated) {
      fetchTasks();
    }

  },[id, keycloak.authenticated])

  if(error != null){
    // tas global error dinksta po 5s, gal vertetu tureti bendra komponenta ilgesniam error'o atvaizdavimui?
    return <SomethingWentWrong onRetry={() => window.location.reload()} />;
  } if(isLoading){
    return <Loading message={ScriptResources.LoadingOrLogin} />;
  } else {
    return (
      <div className="kanban-container">
      <Board cards={cards} setCards={setCards}/>
    </div>
    )
  }
};

const Board = ({cards, setCards}) => {
  return (
    <div className="board">
      <Column
        title={statusMapper[0]}
        column={0}
        headingColor="backlog-color"
        cards={cards}
        setCards={setCards}
      />
      <Column
        title={statusMapper[1]}
        column={1}
        headingColor="todo-color"
        cards={cards}
        setCards={setCards}
      />
      <Column
        title={statusMapper[2]}
        column={2}
        headingColor="in-progress-color"
        cards={cards}
        setCards={setCards}
      />
      <Column
        title={statusMapper[3]}
        column={3}
        headingColor="complete-color"
        cards={cards}
        setCards={setCards}
      />
      <BurnBarrel setCards={setCards} />
    </div>
  );
};

type ColumnProps = {
  title: string;
  headingColor: string;
  cards: CardType[];
  column: StatusEnum;
  setCards: Dispatch<SetStateAction<CardType[]>>;
};

const Column = ({
  title,
  headingColor,
  cards,
  column,
  setCards,
}: ColumnProps) => {
  const [active, setActive] = useState(false);

  const handleDragStart = (e: DragEvent, card: CardType) => {
    e.dataTransfer.setData("cardId", card.id);
  };

  const changeCardStatusInBackend = (card : CardType) => {
    // make patch api call
    console.log("api call to change task status: ");
    console.log(card);
  }

  const handleDragEnd = (e: DragEvent) => {
    const cardId = e.dataTransfer.getData("cardId");

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
          return <Card key={c.id} {...c} handleDragStart={handleDragStart} />;
        })}
        <DropIndicator beforeId={null} column={column} />
        <AddCard column={column} setCards={setCards} />
      </div>
    </div>
  );
};

type CardProps = CardType & {
  handleDragStart: Function;
};


const Card = ({
  name,
  id,
  status,
  handleDragStart,
  description,
  dueDate,
  estimate,
  type,
  priority,
}: CardProps) => {
  const [isOver, setIsOver] = useState(null);
  const [show, setShow] = useState(false);
  const [taskDetails, setTaskDetails] = useState<CardType>({
    name,
    id,
    status,
    fkCreatedByUserId: "00000000-0000-0000-0000-000000000000", 
    fkWorkspaceId: "00000000-0000-0000-0000-000000000000", 
    fkAssignedToUserId: null,
    dueDate,
    description,
    estimate,
    type,
    priority,
  });

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleEditClick = () => {
    handleShow();
    console.log("clicked with id" + id);
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;

    setTaskDetails((prevDetails) => ({
      ...prevDetails,
      [name]: name === "status" || name === "estimate" || name === "type" || name === "priority" 
      ? parseInt(value, 10)
      : value,
    }));
  };

  const handleSave = () => {
    const updatedCard = {
      Name : taskDetails.name,
      Status : taskDetails.status,
      Estimate : taskDetails.estimate,
      Type : taskDetails.type,
      Priority : taskDetails.priority
    }

    console.log("Saving changes:", updatedCard);
    // using pre-set workspace id while workspaces are not implemented
    const cardPatchRequest = (updatedCard) => {
      axiosInstance.patch(`/task/${taskDetails.id}`, updatedCard)
      .then(response => {
        console.log(response);
        const returnedCard = response.data;
        // TODO update cards once response is received.
      })
      .catch(error => {
        console.log(error);
      })
    }

    cardPatchRequest(updatedCard);
    setShow(false);
  };

  return (
    <>
      <DropIndicator beforeId={id} column={status} />
      <motion.div
        layout
        layoutId={id}
        draggable="true"
        onDragStart={(e) => handleDragStart(e, {
          name,
          id,
          status,
          fkCreatedByUserId: taskDetails.fkCreatedByUserId,
          fkWorkspaceId: taskDetails.fkWorkspaceId,
          fkAssignedToUserId: taskDetails.fkAssignedToUserId,
          dueDate: taskDetails.dueDate,
          description: taskDetails.description,
          estimate: taskDetails.estimate,
          type: taskDetails.type,
          priority: taskDetails.priority,
        })}
        onHoverStart={() => setIsOver(id)}
        onHoverEnd={() => setIsOver(null)}
        className="card"
      >
        <p className="card-title">{name}</p>
        {isOver === id && (
          <span onClick={handleEditClick} className="edit-button-wrapper">
            <BsHammer style={{ pointerEvents: 'none' }} />
          </span>
        )}
      </motion.div>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Edit Task</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3" controlId="taskName">
              <Form.Label>Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter task name"
                name="name"
                value={taskDetails.name}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskDescription">
              <Form.Label>Description</Form.Label>
              <Form.Control
                as="textarea"
                rows={3}
                placeholder="Enter description"
                name="description"
                value={taskDetails.description || ""}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskDueDate">
              <Form.Label>Due Date</Form.Label>
              <Form.Control
                type="date"
                name="dueDate"
                value={taskDetails.dueDate}
                onChange={handleInputChange}
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskEstimate">
              <Form.Label>Estimate</Form.Label>
              <Form.Control
                as="select"
                name="estimate"
                value={taskDetails.estimate}
                onChange={handleInputChange}
              >
                <option value={0}>{estimateMapper[0]}</option>
                <option value={1}>{estimateMapper[1]}</option>
                <option value={2}>{estimateMapper[2]}</option>
              </Form.Control>
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskType">
              <Form.Label>Type</Form.Label>
              <Form.Control
                as="select"
                name="type"
                value={taskDetails.type}
                onChange={handleInputChange}
              >
                <option value={0}>{typeMapper[0]}</option>
                <option value={1}>{typeMapper[1]}</option>
                <option value={2}>{typeMapper[2]}</option>
              </Form.Control>
            </Form.Group>
            <Form.Group className="mb-3" controlId="taskPriority">
              <Form.Label>Priority</Form.Label>
              <Form.Control
                as="select"
                name="priority"
                value={taskDetails.priority}
                onChange={handleInputChange}
              >
                <option value={0}>{priorityMapper[0]}</option>
                <option value={1}>{priorityMapper[1]}</option>
                <option value={2}>{priorityMapper[2]}</option>
              </Form.Control>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleSave}>
            Save changes
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

type DropIndicatorProps = {
  beforeId: string | null;
  column: StatusEnum;
};

const DropIndicator = ({ beforeId, column }: DropIndicatorProps) => {
  return (
    <div
      data-before={beforeId || "-1"}
      data-column={column}
      className="drop-indicator"
    />
  );
};

const BurnBarrel = ({
  setCards,
}: {
  setCards: Dispatch<SetStateAction<CardType[]>>;
}) => {
  const [active, setActive] = useState(false);

  const handleDragOver = (e: DragEvent) => {
    e.preventDefault();
    setActive(true);
  };

  const handleDragLeave = () => {
    setActive(false);
  };

  const handleDragEnd = (e: DragEvent) => {
    const cardId = e.dataTransfer.getData("cardId");

    const cardDeletionRequest = (cardId) => {
      axiosInstance.delete(`/task/${cardId}`)
      .then(response => {
        console.log(response);
        if (response.status === 200){
          setCards((pv) => pv.filter((c) => c.id !== cardId));
        }
      })
      .catch(error => {
        console.log(error);
      })
    }

    cardDeletionRequest(cardId);
    setActive(false);
  };

  return (
    <div
      onDrop={handleDragEnd}
      onDragOver={handleDragOver}
      onDragLeave={handleDragLeave}
      className={`burn-barrel ${active ? "burn-barrel-active" : "burn-barrel-inactive"}`}
    >
      {active ? <FaFire className="animate-bounce" /> : <FiTrash />}
    </div>
  );
};

type AddCardProps = {
  column: StatusEnum;
  setCards: Dispatch<SetStateAction<CardType[]>>;
};

const AddCard = ({ column, setCards }: AddCardProps) => {
  const [name, setName] = useState("");
  const [adding, setAdding] = useState(false);

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!name.trim().length) return;

    const newCard: CardType = {
      Status: column,
      Name: name.trim(),
      Estimate: 1, // Default value
      Type: 2, // Default value
      Priority: 2, // Default value
    };

    // using pre-set workspace id while workspaces are not implemented
    const cardCreationRequest = () => {
      axiosInstance.post(`/task/0f2ca3a8-8372-4d7f-bf0f-97e79b922f3c`, newCard)
      .then(response => {
        console.log(response);
        const returnedCard = response.data;
        setCards((pv) => [...pv, returnedCard]);
      })
      .catch(error => {
        console.log(error);
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