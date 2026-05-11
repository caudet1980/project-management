import Task from "./Task";
import CreateTask from "./CreateTask";

export default function Tasks({ tasks = [], addTask, clearTask }) {

    function handleAddTask(task) {
        addTask(task);
    }
    
    return (
        <section class="tasks">
            <h2>Tasks</h2>
            <CreateTask handleAddTask={(task) => handleAddTask(task)} />
            
            {tasks.length === 0 && (
                <p>This project does not have any tasks yet.</p>
            )}
            {tasks.length !==0 && (
                <ul>       
                    {tasks.map((task) => <Task task={task} clearTask={clearTask} />)}
                </ul>
            )}
        </section>
    );
}