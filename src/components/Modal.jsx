import { createPortal } from "react-dom";

import Button from "./Button";

export default function Model({ children, ref, buttonLabel }) {
    return createPortal(
        <dialog ref={ref}>
            {children}
             <form method="dialog">
                <Button label={buttonLabel} />
             </form>
        </dialog>,
        document.getElementById('modal-root')
    );

} 