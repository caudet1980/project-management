export default function Input({ label, input, ...props }) {

    return (
        <p className="input">
            <label>{label}</label>
            {input === 'textarea' ? (
                <textarea {...props} />
            ) : (
                <input {...props} />
            )}
        </p>
    );
}