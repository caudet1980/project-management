import { useState, useRef } from "react";

import Aside from "./components/Aside";
import Header from "./components/Header";
import Tasks from "./components/Tasks";
import CreateProject from "./components/CreateProject";
import EmptyProject from "./components/EmptyProject";

function App() {
  const [projects, setProjects] = useState({
    project: null,
    projects: []
  });

  const [formIsVisible, setFormIsVisible] = useState(false);
  const dialog = useRef();

  function handleOpenForm() {
    setFormIsVisible(true);
  }

  function handleAddProject(newProject) {
    setProjects({ ...projects, projects: [...projects.projects, newProject], project: newProject });
    setFormIsVisible(false);
  }

  function handleDeleteProject(projectId) {
    setProjects({ ...projects, projects: projects.projects.filter((p) => p.id !== projectId), project: null });
  }

  function handleAddTask(description) {
    const newTask = { id: crypto.randomUUID(), description };
    const updatedProject = { ...projects.project, tasks: [...projects.project.tasks, newTask] };
    setProjects({ ...projects, projects: projects.projects.map((p) => p.id === projects.project.id ? updatedProject : p), project: updatedProject });
  }

  function handleClearTask(taskId) {
    const updatedProject = { ...projects.project, tasks: projects.project.tasks.filter((t) => t.id !== taskId) };
    setProjects({ ...projects, projects: projects.projects.map((p) => p.id === projects.project.id ? updatedProject : p), project: updatedProject });
  }

  let sectionContent;
  if (formIsVisible) {
    sectionContent = <CreateProject onCancel={() => setFormIsVisible(false)} onSubmit={handleAddProject} />;
  } else if (projects.project) {
    sectionContent = <>
      <Header project={projects.project} onDelete={handleDeleteProject} />
      <Tasks tasks={projects.project.tasks} addTask={(description) => handleAddTask(description) } clearTask={(taskId) => handleClearTask(taskId)} />
    </>;
  } else {
    sectionContent = <EmptyProject openForm={handleOpenForm} />;
  }
  return (
    <>
      <main>
        <Aside projects={projects} setProject={(project) => setProjects({ ...projects, project })} openForm={handleOpenForm} />
        <div>
          {sectionContent}
        </div>
      </main>
    </>
  );
}

export default App;
