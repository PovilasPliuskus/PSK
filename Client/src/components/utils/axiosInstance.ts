import axios from "axios";
import keycloak from "../../keycloak";

const axiosInstance = axios.create({
    baseURL: "http://localhost:5164/api",
  });

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
    
export default axiosInstance;