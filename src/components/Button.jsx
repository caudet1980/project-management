export default function Button({ label, mode = 'primary', isDelete = false, isActive = false, ...props }) {
    let classes = mode;

    if (isDelete) {
        classes += " danger";
    } else if (isActive) {
        classes += " active";
    }

    return (
        <button className={classes} {...props}>
            {label}
        </button>
    );
}
