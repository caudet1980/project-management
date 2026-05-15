import { useState } from "react";
import Input from "./Input";
import Button from "./Button";
import Toast from "./Toast";
import { useLanguage } from "../context/LanguageContext";

export default function CreateProject({ onCancel, onSubmit }) {
    const { t } = useLanguage();
    const today = new Date().toISOString().split('T')[0];

    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [dueDate, setDueDate] = useState('');
    const [errors, setErrors] = useState({});
    const [toast, setToast] = useState(null);

    function validate(fields = { title, description, dueDate }) {
        const newErrors = {};
        if (!fields.title) newErrors.title = true;
        if (!fields.description) newErrors.description = true;
        if (!fields.dueDate || fields.dueDate < today) newErrors.dueDate = true;
        return newErrors;
    }

    function handleChange(field, value) {
        const updated = { title, description, dueDate, [field]: value };
        if (field === 'title') setTitle(value);
        if (field === 'description') setDescription(value);
        if (field === 'dueDate') setDueDate(value);
        if (errors[field]) setErrors(prev => ({ ...prev, [field]: !!validate(updated)[field] }));
    }

    function handleSubmit(e) {
        e.preventDefault();
        const newErrors = validate();
        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            setToast({ message: t('formError'), type: 'error' });
            return;
        }
        onSubmit({
            id: crypto.randomUUID(),
            title,
            description,
            dueDate: new Date(dueDate),
            tasks: [],
        });
    }

    return (
        <div className="create-project card">
            <form onSubmit={handleSubmit} onReset={onCancel}>
                <menu>
                    <li><Button type="reset" mode="secondary" isDelete label={t('cancel')} /></li>
                    <li><Button type="submit" mode="primary" label={t('save')} /></li>
                </menu>
                <div className="form-row">
                    <Input
                        label={t('projectTitle')}
                        value={title}
                        onChange={(e) => handleChange('title', e.target.value)}
                        isInvalid={errors.title}
                    />
                    <Input
                        label={t('projectDueDate')}
                        type="date"
                        min={today}
                        value={dueDate}
                        onChange={(e) => handleChange('dueDate', e.target.value)}
                        isInvalid={errors.dueDate}
                    />
                </div>
                <Input
                    label={t('projectDescription')}
                    input="textarea"
                    value={description}
                    onChange={(e) => handleChange('description', e.target.value)}
                    isInvalid={errors.description}
                />
            </form>
            <Toast message={toast?.message} type={toast?.type} onClose={() => setToast(null)} />
        </div>
    );
}
