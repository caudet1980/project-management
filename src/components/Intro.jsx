export default function Intro({ title, description }) {
    return (
        <>
            <h2>{title}</h2>
            <p className="intro-desc">{description}</p>
        </>
    );
}