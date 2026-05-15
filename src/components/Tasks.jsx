import Task from "./Task";
import CreateTask from "./CreateTask";
import { useLanguage } from "../context/LanguageContext";

export default function Tasks({ tasks = [], addTask, clearTask }) {
    const { t } = useLanguage();

    return (
        <section className="tasks card">
            <h2 className="section-label">{t('tasks')}</h2>
            <CreateTask onAddTask={addTask} />

            {tasks.length === 0 && <p>{t('noTasks')}</p>}
            {tasks.length !== 0 && (
                <ul>
                    {tasks.map((task) => <Task key={task.id} task={task} clearTask={clearTask} />)}
                </ul>
            )}
        </section>
    );
}
