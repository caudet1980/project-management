import { useState, useRef } from "react";

import Input from "./Input";
import Modal from "./Modal";
import Errors from "./Errors";
import Button from "./Button";

export default function CreateProject({onCancel, onSubmit}) {
    const dialog = useRef();

    const titleRef = useRef('');
    const descriptionRef = useRef('');
    const dueDateRef = useRef(new Date().toISOString().split('T')[0]);
    const [errors, setErrors] = useState([]);
    const [project, setProject] = useState({
        id: crypto.randomUUID(),
        title: titleRef.current.value, 
        description: descriptionRef.current.value, 
        dueDate: dueDateRef.current.value ? new Date(dueDateRef.current.value) : null,
        tasks: [],
    });

    function handleSubmit(e) {
        e.preventDefault();
        
        const newErrors = [];
        if(!project.title) {
            newErrors.push('Please enter a title for your project');
        }

        if(!project.description) {
            newErrors.push('Please enter a description for your project');
        }

        if(!project.dueDate) {
            newErrors.push('Please enter a due date for your project');
        }

        setErrors(newErrors);

        if(newErrors.length > 0) {
            dialog.current.showModal();
            return;
        }

        onSubmit(project);
    }

    return(
        <div className="create-project">
            <Modal ref={dialog} buttonLabel="Close">
                <Errors errors={errors } />
            </Modal>
            <form method="dialog" onSubmit={handleSubmit} onReset={onCancel}>
                <menu>
                    <li><Button type="reset" mode="light" isDelete label="Cancel" /></li>
                    <li><Button type="submit" label="Save" /></li>
                </menu>
            </form>
            <div>
                <Input 
                    label="Title" 
                    input="input"
                    ref={titleRef}
                    onChange={() => setProject({...project, title: titleRef.current.value})}
                />
                <Input 
                    label="Description" 
                    input="textarea"
                    ref={descriptionRef}
                    onChange={() => setProject({...project, description: descriptionRef.current.value})}
                />
                <Input 
                    label="Due Date" 
                    input="input"
                    ref={dueDateRef}
                    onChange={() => setProject({...project, dueDate: dueDateRef.current.value ? new Date(dueDateRef.current.value) : null})}
                    type="date" 
                />
              </div>
        </div>
    );
}