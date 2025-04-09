import axios from "axios";
import keycloak from "../keycloak";
import { useEffect } from "react";
import { useError } from "../components/base/ErrorContext";

const axiosInstance = axios.create({
  baseURL: "http://localhost:5164/api",
});

const useAxiosInterceptor = () => {
  const { setError } = useError();

  useEffect(() => {
    // Response interceptor
    const responseInterceptor = axiosInstance.interceptors.response.use(
      (response) => response, // Pass through successful responses
      (error) => {
        if (error.response) {
          // Response exists, handle based on status code
          if (error.response.status === 401) {
            setError("You are not authorized to perform this action. Please log in and try again.");
            // Don't redirect here - let the application handle it
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

    // Cleanup function to eject the interceptor when the component unmounts
    return () => {
      axiosInstance.interceptors.response.eject(responseInterceptor);
    };
  }, [setError]);
};

// Request interceptor - defined once, not in the hook
const requestInterceptor = axiosInstance.interceptors.request.use(
  async (config) => {
    if (!keycloak.authenticated) {
      // Instead of redirecting here, just reject the promise
      return Promise.reject(new Error("User not authenticated"));
    }

    // If the token is expired, try to refresh it
    if (keycloak.isTokenExpired()) {
      try {
        await keycloak.updateToken(30);
      } catch (error) {
        console.error("Failed to refresh token", error);
        // Instead of redirecting here, just reject the promise
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

export { axiosInstance, useAxiosInterceptor };