import React, {
  Dispatch,
  SetStateAction,
  useState,
  DragEvent,
  useEffect,
} from "react";
import { FiTrash } from "react-icons/fi";
import { FaFire } from "react-icons/fa";
import './TaskBoardViewPage.css';
import { statusMapper } from "../../utility/enumMapper.tsx";
import { TaskSummary } from "../../../Models/TaskSummary.ts";
import { axiosInstance } from '../../../utils/axiosInstance';
import { useParams } from "react-router-dom";
import keycloak from '../../../keycloak';
import SomethingWentWrong from "../../base/SomethingWentWrong.tsx";
import Loading from "../../base/Loading.tsx";
import ScriptResources from "../../../assets/resources/strings.ts";
import Column from "./Column.tsx";

export const TaskBoardViewPage = () => {
  const { id } = useParams();
  const [cards, setCards] = useState<TaskSummary[]>([]);
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

type BoardProps = {
  cards: TaskSummary[],
  setCards: Dispatch<React.SetStateAction<TaskSummary[]>>
}

const Board: React.FC<BoardProps> = ({cards, setCards}) => {
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

type BurnBarrelProps = {
  setCards: Dispatch<SetStateAction<TaskSummary[]>>
};

const BurnBarrel : React.FC<BurnBarrelProps> = ({setCards}) => {
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

    const cardDeletionRequest = (cardId : string) => {
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