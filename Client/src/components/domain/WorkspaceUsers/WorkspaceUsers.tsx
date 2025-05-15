// SITAS BUS TVARKOMAS. CIA TIK TEMPLATE KURI REIKES PAKEISTI
import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate } from 'react-router-dom';
import { Table, Modal, Button, Form } from 'react-bootstrap';
import Pagination from '../../base/Pagination';
import keycloak from '../../../keycloak';
import ScriptResources from '../../../assets/resources/strings';
import { axiosInstance } from '../../../utils/axiosInstance';
import { Workspace } from "../../../Models/Workspace";
import SomethingWentWrong from '../../base/SomethingWentWrong';
import Loading from '../../base/Loading';

const Workspaces: React.FC = () => {
    const [workspaces, setWorkspaces] = useState<Workspace[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalItems, setTotalItems] = useState(0);
    const [pageSize, setPageSize] = useState(10);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const [showModal, setShowModal] = useState(false);
    const [newWorkspaceName, setNewWorkspaceName] = useState('');
    const [alertMessage, setAlertMessage] = useState<string | null>(null);
    const [alertVariant, setAlertVariant] = useState<'success' | 'danger' | null>(null);
    const [showEditModal, setShowEditModal] = useState(false);
    const [workspaceToEdit, setWorkspaceToEdit] = useState<Workspace | null>(null);
    const [editedName, setEditedName] = useState('');


    const fetchItems = () => {
        setError(null);
        const loadingTimeout = setTimeout(() => {
            setIsLoading(true);
          }, 500);
        axiosInstance.get(`/workspace`, {
            params: { pageNumber: currentPage, pageSize: pageSize },
        })
        .then(response => {
            setWorkspaces(response.data.workspaces.items || []);
            setTotalPages(response.data.workspaces.totalPages || 0);
            setTotalItems(response.data.workspaces.totalItems || 0);
        })
        .catch(error => {
            setError(error);
            console.error(ScriptResources.ErrorFetchingWorkspaces, error);
            setWorkspaces([]);
        })
        .finally(() => {
            clearTimeout(loadingTimeout);
            setIsLoading(false);
        });
    };

    useEffect(() => {
        if (keycloak.authenticated) {
            fetchItems();
        }
    }, [currentPage, pageSize, keycloak.authenticated]);

    if (error !== null)
    {
        return <SomethingWentWrong onRetry={() => window.location.reload()} />;
    }

    if (isLoading && !keycloak.authenticated) {
        return <Loading message={ScriptResources.LoadingOrLogin} />;
    }

    // Delete workspace
    const handleDelete = (workspaceId: string) => {
        if (window.confirm(ScriptResources.DeleteConfirmation)) {
            axiosInstance.delete(`/workspace/${workspaceId}`)
                .then(() => {
                    setWorkspaces(workspaces.filter(workspace => workspace.id !== workspaceId));
                    setAlertMessage(ScriptResources.WorkspaceDeleted);
                    setAlertVariant('success');
                    setTimeout(() => {
                        setAlertMessage(null);
                    }, 3000); // Clear alert after 3 seconds
                })
                .catch(error => {
                    console.error('Error deleting workspace:', error);
                    setAlertMessage(ScriptResources.ErrorDeletingWorkspace);
                    setAlertVariant('danger');
                });
        }
    };

    // Create new workspace
    const handleModalShow = () => setShowModal(true);

    const handleModalClose = () => {
        setShowModal(false);
        setNewWorkspaceName('');
    };
    
    return (
        <div className="m-5">
            {alertMessage && (
                <div className={`alert alert-${alertVariant} alert-dismissible fade show`} role="alert">
                    {alertMessage}
                </div>
            )}
            <p>{ScriptResources.DoubleClickToSeeAllTasks}</p>
            <button className="btn btn-primary mb-3" onClick={handleModalShow}>
                <span className="material-icons me-2" style={{verticalAlign: 'middle'}}>add</span>
                {ScriptResources.CreateNew}
            </button>
            <h2>{ScriptResources.Workspaces}</h2>

            <Table striped bordered hover>
                <thead>
                <tr>
                    <th>{ScriptResources.WorkspaceName}</th>
                </tr>
                </thead>
                <tbody>
                {workspaces.map((workspace) => (
                    <tr key={workspace.id}>
                        <td>{workspace.name}</td>
                        <td>
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