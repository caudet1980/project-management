import { createContext, useContext, useState } from 'react';
import fr from '../locales/fr.json';
import en from '../locales/en.json';

const translations = { fr, en };

const LanguageContext = createContext(null);

export function LanguageProvider({ children }) {
    const [language, setLanguage] = useState('fr');
    const t = (key) => translations[language][key] ?? key;
    const toggle = () => setLanguage(l => l === 'fr' ? 'en' : 'fr');

    return (
        <LanguageContext.Provider value={{ language, t, toggle }}>
            {children}
        </LanguageContext.Provider>
    );
}

export function useLanguage() {
    const context = useContext(LanguageContext);
    if (!context) throw new Error('useLanguage must be used within a LanguageProvider');
    return context;
}
