import axios from "axios";

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
});

api.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
});

api.interceptors.response.use(
    (response) => response, 
    (error) => {
    if (error.response && error.response.status === 401) {
        localStorage.removeItem('token');
        window.location.href = '/login';
    }
    return Promise.reject(error);
});

export const authApi = {
    register: (data) => 
        api.post('/auth/register', data),
    login: (data) => 
        api.post('/auth/login', data),
};

export const projectsApi = {
    getAll: () => api.get('/projects'),
    getById: (id) => api.get(`/projects/${id}`),
    create: (data) =>
        api.post('/projects', data),
    delete: (id) => api.delete(`/projects/${id}`),
};

export const tasksApi = {
    create: (projectId, data) => api.post(`/projects/${projectId}/tasks`, data),
    delete: (projectId, taskId) => api.delete(`/projects/${projectId}/tasks/${taskId}`),
};