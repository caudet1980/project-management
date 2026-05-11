import Button from "./Button";

export default function Task({ task, clearTask }) {
    return (
        <li className="task" key={task.id}>
            <p>{task.description}</p>
            <Button label="Clear" mode="light" isDelete onClick={() => clearTask(task.id)} />
       </li>
    );
}