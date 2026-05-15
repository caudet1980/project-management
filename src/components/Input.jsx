export default function Input({ label, input, isInvalid, ...props }) {

    return (
        <div className={`input ${isInvalid ? 'input--invalid' : ''}`}>
            <label>{label}</label>
            {input === 'textarea' ? (
                <textarea className="field-input" {...props} />
            ) : (
                <input className="field-input" {...props} />
            )}
        </div>
    );
}