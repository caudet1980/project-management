import Button from "./Button";

export default function Task({ task, clearTask }) {
    return (
        <li className="task">
            <div className="task-checkbox" />
            <p>{task.description}</p>
            <Button label="✓" mode="secondary" isDelete onClick={() => clearTask(task.id)} />
        </li>
    );
}
