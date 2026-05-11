export default function Button({ label, mode = 'dark', isDelete = false, isActive = false, openForm, ...props }) {
    let classes = mode;

    if(mode === 'light') {
        classes = "light";
    }

    if(isDelete) {
        classes += " delete";
    } else if(mode === 'light') {
        classes += " hover:text-stone-700";
    } else if(isActive) {
        classes += " active";
    }

    return (
        <button 
            className={classes}
            onClick={openForm}
            {...props}
        >
            {label}
        </button>
    );
}