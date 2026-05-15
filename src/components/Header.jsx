import Button from "./Button";
import { useLanguage } from "../context/LanguageContext";

export default function Header({ project, onDelete }) {
    const { t, language } = useLanguage();
    const formattedDate = new Date(project.dueDate).toLocaleDateString(language === 'fr' ? 'fr-CA' : 'en-US', {
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
                <Button label={t('delete')} mode="secondary" isDelete onClick={() => onDelete(project.id)} />
            </div>
            <p>{project.description}</p>
        </header>
    );
}