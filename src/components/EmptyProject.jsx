import Button from "./Button";
import Intro from "./Intro";
import TaskManagerIllustration from "./TaskManagerIllustration";
import { useLanguage } from "../context/LanguageContext";

export default function EmptyProject({ openForm }) {
    const { t } = useLanguage();

    return (
        <div className="empty-project">
            <TaskManagerIllustration />
            <Intro title={t('noProjectTitle')} description={t('noProjectDesc')} />
            <Button label={t('createProject')} onClick={openForm} />
        </div>
    );
}
