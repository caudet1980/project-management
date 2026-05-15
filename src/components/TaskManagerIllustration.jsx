export default function TaskManagerIllustration() {
    return (
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 280 220" fill="none" aria-hidden="true">
            {/* Main card */}
            <rect x="20" y="10" width="240" height="200" rx="14" fill="#ffffff" stroke="#e2e0dd" strokeWidth="1.5"/>
            {/* Header */}
            <rect x="20" y="10" width="240" height="48" rx="14" fill="#6366f1"/>
            <rect x="20" y="38" width="240" height="20" fill="#6366f1"/>
            {/* Window dots */}
            <circle cx="45" cy="34" r="5" fill="rgba(255,255,255,0.35)"/>
            <circle cx="63" cy="34" r="5" fill="rgba(255,255,255,0.35)"/>
            <circle cx="81" cy="34" r="5" fill="rgba(255,255,255,0.35)"/>
            {/* Header title */}
            <rect x="108" y="29" width="72" height="10" rx="5" fill="rgba(255,255,255,0.5)"/>
            {/* Task 1 - done */}
            <rect x="40" y="74" width="18" height="18" rx="5" fill="#6366f1"/>
            <path d="M44 83 L47 86 L54 79" stroke="white" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
            <rect x="68" y="79" width="110" height="8" rx="4" fill="#e2e0dd"/>
            {/* Task 2 - done */}
            <rect x="40" y="104" width="18" height="18" rx="5" fill="#6366f1"/>
            <path d="M44 113 L47 116 L54 109" stroke="white" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
            <rect x="68" y="109" width="85" height="8" rx="4" fill="#e2e0dd"/>
            {/* Task 3 - pending */}
            <rect x="40" y="134" width="18" height="18" rx="5" fill="none" stroke="#e2e0dd" strokeWidth="1.5"/>
            <rect x="68" y="139" width="130" height="8" rx="4" fill="#f5f4f2"/>
            {/* Task 4 - pending */}
            <rect x="40" y="164" width="18" height="18" rx="5" fill="none" stroke="#e2e0dd" strokeWidth="1.5"/>
            <rect x="68" y="169" width="95" height="8" rx="4" fill="#f5f4f2"/>
            {/* Floating badge */}
            <rect x="168" y="124" width="76" height="44" rx="10" fill="white" stroke="#e2e0dd" strokeWidth="1.5"/>
            <circle cx="185" cy="146" r="10" fill="#f0fdf4"/>
            <path d="M180 146 L183.5 149.5 L190 142" stroke="#22c55e" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round"/>
            <rect x="200" y="140" width="34" height="6" rx="3" fill="#e2e0dd"/>
            <rect x="200" y="150" width="24" height="5" rx="2.5" fill="#f5f4f2"/>
        </svg>
    );
}
