import Button from "./Button";

export default function Aside({ projects, setProject, openForm }) {
    return (
        <aside>
            <h2 className="light">Your projects</h2>
            <div>
                <Button label="+ Add Project" openForm={openForm} />
            </div>
            <ul>
                {projects.projects.map((project) => {
                    return (
                        <li key={project.id}>
                            <Button 
                                mode="list" onClick={() => setProject(project)} 
                                label={project.title} isActive={projects.project === project}/>                            
                        </li>
                    );
                })}
            </ul>    
        </aside>
    );
}