import Button from "./Button";
import Intro from "./Intro";

export default function EmptyProject({ openForm }) {
    return (
        <div className="empty-project">
            <img src="logo.png" />
            <Intro title="No Project Selected" description="Select a project or get started with a new one" />
            <Button label="Create new project" openForm={openForm} />
        </div>
    );
}