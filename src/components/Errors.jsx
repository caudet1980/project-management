import Intro from "./Intro";

export default function Errors({ errors = [] }) {
    return (
        <>
            <Intro title="Invalid Input" description="Please correct the errors below" />     
            <ul className="error-list">
                {errors.length > 0 && errors.map((error, index) => <li key={index}>{error}</li>)}
            </ul>
        </>
    );
}