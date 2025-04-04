import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate } from 'react-router-dom';
import { Table, Container } from 'react-bootstrap';
import Pagination from '../../base/Pagination';
import keycloak from '../../../keycloak';
import ScriptResources from '../../../assets/resources/strings';
import { axiosInstance } from '../../../utils/axiosInstance';
import { Workspace } from "../../../Models/Workspace";

const Workspaces: React.FC = () => {
    const [workspaces, setWorkspaces] = useState<Workspace[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalItems, setTotalItems] = useState(0);
    const [pageSize, setPageSize] = useState(10);
    const [isLoading, setIsLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchItems = async () => {
            setIsLoading(true);
            try {
                const response = await axiosInstance.get(`/workspaces`, {
                    params: { pageNumber: currentPage, pageSize: pageSize },
                });
                setWorkspaces(response.data.data || []);
                setTotalPages(response.data.totalPages || 0);
                setTotalItems(response.data.totalItems || 0);
            } catch (error) {
                console.error(ScriptResources.ErrorFetchingWorkspaces, error);
                setWorkspaces([]);
            } finally {
                setIsLoading(false);
            }
        };

        if (keycloak.authenticated) {
            fetchItems();
        }
    }, [currentPage, pageSize, keycloak.authenticated]);

    if (isLoading && !keycloak.authenticated) {
        return (
            <Container className="mt-5">
                <h1>{ScriptResources.LoadingOrLogin}</h1>
            </Container>
        );
    }

    const handleCreateNew = () => {
        navigate('/workspaces/new');
    };

    const handleIconClick = (workspaceId: string) => {
        // Turėtų naviguoti į workspace readagavimo puslapį, kuriame bus visi task'ai parodyti. Kosto dalis.
        navigate(`/workspaces/${workspaceId}`);
    };

    const handleDelete = (workspaceId: string) => {
        alert("Delete functionality is not implemented yet.");
    }
    
    return (
        <div className="m-5">
            <button className="btn btn-primary mb-3" onClick={handleCreateNew}>
                <span className="material-icons me-2" style={{verticalAlign: 'middle'}}>add</span>
                {ScriptResources.CreateNew}
            </button>
            <h2>{ScriptResources.Workspaces}</h2>

            <Table striped bordered hover>
                <thead>
                <tr>
                    <th>{ScriptResources.WorkspaceId}</th>
                    <th>{ScriptResources.WorkspaceName}</th>
                    <th>{ScriptResources.CreatedBy}</th>
                    <th>{ScriptResources.CreatedAt}</th>
                </tr>
                </thead>
                <tbody>
                {workspaces.map((workspace) => (
                    <tr key={workspace.id}
                        onDoubleClick={() => handleIconClick(workspace.id)}>
                        <td>{workspace.id}</td>
                        <td>{workspace.name}</td>
                        <td>{workspace.createdBy}</td>
                        <td>{new Date(workspace.createdAt).toLocaleString()}</td>
                        <td>
                                <span
                                    className="material-icons"
                                    style={{cursor: 'pointer'}}
                                    onClick={() => handleIconClick(workspace.id)}
                                >
                                    open_in_new
                                </span>
                                <span
                                    className="material-icons"
                                    style={{cursor: 'pointer', marginRight: '10px'}}
                                    onClick={() => handleDelete(workspace.id)}
                                >
                                        delete
                                </span>
                        </td>
                    </tr>
                ))}
                </tbody>
            </Table>

            <Pagination
                currentPage={currentPage}
                totalPages={totalPages}
                totalItems={totalItems}
                pageSize={pageSize}
                onPageChange={(page) => setCurrentPage(page)}
                onPageSizeChange={(size) => setPageSize(size)}
            />
        </div>
    );
};

export default Workspaces;