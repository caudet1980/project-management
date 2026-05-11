import Button from "./Button";

export default function Header({ project, onDelete }) {
    const formattedDate = project.dueDate.toLocaleDateString('en-US', {
        month: 'short',
        day: 'numeric',
        year: 'numeric',
    });
    
    return (
        <header>
            <div>
                <div className="intro">
                    <h1>{project.title}</h1>
                    <p>{formattedDate}</p>
                </div>
                <Button label="Delete" mode="light" isDelete onClick={() => onDelete(project.id)} />
            </div>
            <p>{project.description}</p>
        </header>
    );
}