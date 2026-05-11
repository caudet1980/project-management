import { useState } from "react";

import Button from "./Button";

export default function CreateTask({ handleAddTask }) {
    const [task, setTask] = useState('');

    function handleOnClick() {
        handleAddTask(task);
        setTask('');
    }

    return (
        <div className="create-task">
            <input value={task} onChange={(e) => setTask(e.target.value)} />
            <Button label="Add Task" mode="light" onClick={handleOnClick} />
        </div>
    );
}