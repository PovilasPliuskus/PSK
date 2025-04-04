import axios from "axios";
import keycloak from "../keycloak";
import { useError } from "../components/base/ErrorContext";

const axiosInstance = axios.create({
    baseURL: "http://localhost:5164/api",
  });

const useAxiosInterceptor = () => {
  const { setError } = useError();

  // RESPONSE INTERCEPTOR
  axiosInstance.interceptors.response.use(
    (response) => response, // Pass through successful responses
    (error) => {

        if (error.response) {
            // Response exists, handle based on status code
            if (error.response.status === 401) {
                setError("You are not authorized to perform this action. Please log in and try again.");
            } else {
                // Handle other server errors
                const message =
                    error.response.data?.title ||
                    error.response.data?.message ||
                    "An error occurred."; // Default message if none is found
                setError(message);
            }
        } else {
            // No response, likely a network error
            console.error("Error details:", error); // Debugging network errors
            setError("A network error occurred. Please try again.");
        }
        return Promise.reject(error); // Reject the promise so further handling can occur
    }
  );

  axiosInstance.interceptors.request.use(
    async (config) => {
        if (!keycloak.authenticated) {
            keycloak.login(); // Redirect to login
            return Promise.reject(new Error("User not authenticated"));
        }

        // If the token is expired, try to refresh it
        if (keycloak.isTokenExpired()) {
            try {
                await keycloak.updateToken(30);
            } catch (error) {
                console.error("Failed to refresh token", error);
                keycloak.login(); // Redirect to login if token refresh fails
                return Promise.reject(new Error("Failed to refresh token"));
            }
        }

        const token = keycloak.token;
    
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
    
        return config;
      },
      (error) => Promise.reject(error)
    );
}
    
export { axiosInstance, useAxiosInterceptor };