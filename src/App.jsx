import { projectsApi, tasksApi } from "./api/api.js";
import { useState, useEffect } from "react";
import { useAuth } from "./context/AuthContext.jsx";
import { useLanguage } from "./context/LanguageContext.jsx";
import Button from "./components/Button.jsx";

import Aside from "./components/Aside";
import Header from "./components/Header";
import Tasks from "./components/Tasks";
import CreateProject from "./components/CreateProject";
import EmptyProject from "./components/EmptyProject";
import LoginPage from "./pages/LoginPage.jsx";
import Toast from "./components/Toast.jsx";

function App() {
  const { isAuthenticated } = useAuth();
  const { language, toggle, t } = useLanguage();
  const [projects, setProjects] = useState({ project: null, projects: [] });
  const [formIsVisible, setFormIsVisible] = useState(false);
  const [toast, setToast] = useState(null);

  useEffect(() => {
    if (!isAuthenticated) return;
    async function loadProjects() {
      try {
        const response = await projectsApi.getAll();
        setProjects({ project: null, projects: response.data });
      } catch (error) {
        setToast({ message: error.message, type: "error" });
      }
    }
    loadProjects();
  }, [isAuthenticated]);

  if (!isAuthenticated) {
    return <LoginPage />;
  }

  async function handleAddProject(projectData) {
    try {
      const response = await projectsApi.create({
        title: projectData.title,
        description: projectData.description,
        dueDate: projectData.dueDate,
      });
      const newProject = response.data;
      setProjects({ ...projects, projects: [...projects.projects, newProject], project: newProject });
      setFormIsVisible(false);
    } catch (error) {
      setToast({ message: error.message, type: "error" });
    }
  }

  async function handleDeleteProject(projectId) {
    try {
      await projectsApi.delete(projectId);
      setProjects({ ...projects, projects: projects.projects.filter((p) => p.id !== projectId), project: null });
      setToast({ message: t('deleteSuccess'), type: "success" });
    } catch (error) {
      setToast({ message: error.message, type: "error" });
    }
  }

  async function handleAddTask(description) {
    try {
      const response = await tasksApi.create(projects.project.id, { description });
      const newTask = response.data;
      const updatedProject = { ...projects.project, tasks: [...projects.project.tasks, newTask] };
      setProjects({ ...projects, projects: projects.projects.map((p) => p.id === projects.project.id ? updatedProject : p), project: updatedProject });
    } catch (error) {
      setToast({ message: error.message, type: "error" });
    }
  }

  async function handleClearTask(taskId) {
    try {
      await tasksApi.delete(projects.project.id, taskId);
      const updatedProject = { ...projects.project, tasks: projects.project.tasks.filter((t) => t.id !== taskId) };
      setProjects({ ...projects, projects: projects.projects.map((p) => p.id === projects.project.id ? updatedProject : p), project: updatedProject });
    } catch (error) {
      setToast({ message: error.message, type: "error" });
    }
  }

  let sectionContent;
  if (formIsVisible) {
    sectionContent = <CreateProject onCancel={() => setFormIsVisible(false)} onSubmit={handleAddProject} />;
  } else if (projects.project) {
    sectionContent = <>
      <Header project={projects.project} onDelete={handleDeleteProject} />
      <Tasks tasks={projects.project.tasks} addTask={handleAddTask} clearTask={handleClearTask} />
    </>;
  } else {
    sectionContent = <EmptyProject openForm={() => setFormIsVisible(true)} />;
  }

  return (
    <>
      <Button label={language === 'fr' ? 'EN' : 'FR'} mode="secondary" onClick={toggle} className="lang-toggle" />
      <main>
        <Aside projects={projects} setProject={(project) => setProjects({ ...projects, project })} openForm={() => setFormIsVisible(true)} />
        <div>{sectionContent}</div>
      </main>
      <Toast message={toast?.message} type={toast?.type} onClose={() => setToast(null)} />
    </>
  );
}

export default App;
