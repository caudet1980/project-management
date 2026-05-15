import Button from "./Button";
import { useAuth } from "../context/AuthContext";
import { useLanguage } from "../context/LanguageContext";

export default function Aside({ projects, setProject, openForm }) {
    const { logout } = useAuth();
    const { t } = useLanguage();

    return (
        <aside>
            <div>
                <h2 className="light">{t('yourProjects')}</h2>
                <div>
                    <Button label={t('addProject')} onClick={openForm} />
                </div>
                <ul>
                    {projects.projects.map((project) => (
                        <li key={project.id}>
                            <Button
                                mode="list" onClick={() => setProject(project)}
                                label={project.title} isActive={projects.project === project} />
                        </li>
                    ))}
                </ul>
            </div>
            <Button label={t('logout')} mode="secondary" isDelete onClick={logout} />
        </aside>
    );
}