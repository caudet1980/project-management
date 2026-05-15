import { useEffect } from "react";
import Button from "./Button";

export default function Toast({ message, type, onClose, duration = 5000 }) {
    useEffect(() => {
        if(message) {
            const timer = setTimeout(() => {
                onClose();
            }, duration);
            return () => clearTimeout(timer);
        }
    }, [message, onClose, duration]);

    if(!message) return null;

    return (
        <div className={"toast toast--"+type}>
            <span>{message}</span>
            <Button label="×" mode="light" onClick={onClose} />
        </div>
    );
};