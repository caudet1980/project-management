import { useState } from "react";
import Button from "./Button";
import Toast from "./Toast";
import { useLanguage } from "../context/LanguageContext";

export default function CreateTask({ onAddTask }) {
    const { t } = useLanguage();
    const [task, setTask] = useState('');
    const [toast, setToast] = useState(null);

    function handleAdd() {
        if (!task.trim()) {
            setToast({ message: t('taskRequired'), type: 'error' });
            return;
        }
        onAddTask(task.trim());
        setTask('');
    }

    return (
        <div className="create-task">
            <input className="field-input" placeholder={t('taskPlaceholder')} value={task} onChange={(e) => setTask(e.target.value)} />
            <Button label={t('addTask')} mode="secondary" onClick={handleAdd} />
            <Toast message={toast?.message} type={toast?.type} onClose={() => setToast(null)} />
        </div>
    );
}
