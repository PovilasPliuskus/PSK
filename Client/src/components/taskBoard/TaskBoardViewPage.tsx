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

export const TaskBoardViewPage = () => {
  const { id } = useParams();
  const [cards, setCards] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  
  useEffect(() => {
    const fetchTasks = () => {
      setError(null);
      setIsLoading(true);
      axiosInstance.get(`/task/${id}`)
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
    return(
      <h2>Loading... </h2>
    ) 
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
    // make api patch call
    console.log("Saving changes:", taskDetails);
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

  const deleteCardBackend = (cardId) => {
    console.log("api call to delete card with id: ");
    console.log(cardId);
  }

  const handleDragEnd = (e: DragEvent) => {
    const cardId = e.dataTransfer.getData("cardId");

    setCards((pv) => pv.filter((c) => c.id !== cardId));
    deleteCardBackend(cardId);
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

const addCardBackend = (card : CardType) => {
  console.log("Api call to add card: ");
  console.log(card);
}

const AddCard = ({ column, setCards }: AddCardProps) => {
  const [name, setName] = useState("");
  const [adding, setAdding] = useState(false);

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!name.trim().length) return;

    const newCard: CardType = {
      status: column,
      name: name.trim(),
      id: Math.random().toString(),
      fkCreatedByUserId: "00000000-0000-0000-0000-000000000000", // Placeholder
      fkWorkspaceId: "00000000-0000-0000-0000-000000000000", // Placeholder
      fkAssignedToUserId: null,
      dueDate: null,
      description: null,
      estimate: 1, // Default value
      type: 2, // Default value
      priority: 2, // Default value
    };
    setCards((pv) => [...pv, newCard]);

    addCardBackend(newCard);

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


const DEFAULT_CARDS: CardType[] = [
  // BACKLOG
  {
    name: "Look into render bug in dashboard",
    id: "1",
    status: 0,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: null,
    description: "Investigate why the dashboard is not rendering correctly.",
    estimate: 2,
    type: 2,
    priority: 2,
  },
  {
    name: "SOX compliance checklist",
    id: "2",
    status: 0,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: new Date("2025-04-15"),
    description: "Review and update the SOX compliance checklist for the upcoming audit.",
    estimate: 2,
    type: 2,
    priority: 2,
  },
  {
    name: "[SPIKE] Migrate to Azure",
    id: "3",
    status: 0,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: null,
    description: "Research and plan the migration of our infrastructure to Azure.",
    estimate: 2,
    type: 1,
    priority: 0,
  },
  {
    name: "Document Notifications service",
    id: "4",
    status: 0,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: new Date("2025-04-10"),
    description: "Create comprehensive documentation for the Notifications service.",
    estimate: 1,
    type: 0,
    priority: 1,
  },
  // TODO
  {
    name: "Research DB options for new microservice",
    id: "5",
    status: 1,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: null,
    description: "Explore different database options suitable for the new microservice.",
    estimate: 0,
    type: 2,
    priority: 2,
  },
  {
    name: "Postmortem for outage",
    id: "6",
    status: 1,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: new Date("2025-04-05"),
    description: "Conduct a postmortem analysis for the recent service outage.",
    estimate: 1,
    type: 0,
    priority: 2,
  },
  {
    name: "Sync with product on Q3 roadmap",
    id: "7",
    status: 1,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: new Date("2025-04-04"),
    description: "Schedule a meeting to align with the product team on the Q3 roadmap.",
    estimate: 0,
    type: 0,
    priority: 1,
  },

  // DOING
  {
    name: "Refactor context providers to use Zustand",
    id: "8",
    status: 2,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: null,
    description: "Refactor the existing React context providers to use the Zustand library for state management.",
    estimate: 2,
    type: 0,
    priority: 2,
  },
  {
    name: "Add logging to daily CRON",
    id: "9",
    status: 2,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: null,
    description: "Implement logging for the daily CRON job to track its execution and identify potential issues.",
    estimate: 0,
    type: 1,
    priority: 1,
  },
  // DONE
  {
    name: "Set up DD dashboards for Lambda listener",
    id: "10",
    status: 3,
    fkCreatedByUserId: "a1b2c3d4-e5f6-7890-1234-567890abcdef",
    fkWorkspaceId: "f9e8d7c6-b5a4-3210-fedc-ba9876543210",
    fkAssignedToUserId: null,
    dueDate: null,
    description: "Configure Datadog dashboards to monitor the performance and health of the Lambda listener function.",
    estimate: 1,
    type: 0,
    priority: 1,
  },
];