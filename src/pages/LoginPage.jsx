import { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { useLanguage } from '../context/LanguageContext';
import { useNavigate } from 'react-router-dom';
import { authApi } from '../api/api';
import Input from '../components/Input';
import Button from '../components/Button';
import Toast from '../components/Toast';

export default function LoginPage() {
    const [isLogin, setIsLogin] = useState(true);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [toast, setToast] = useState({message: null, type: null});
    const [errors, setErrors] = useState({});

    const { login } = useAuth();
    const { t, language, toggle } = useLanguage();
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setToast({ message: null });

        let err = {};
        if (!email) err.email = true;
        if (!password) err.password = true;

        if (Object.keys(err).length > 0) {
            setErrors(err);
            setToast({ message: t('formError'), type: "error" });
            return;
        }
        setErrors({});

        try {
            const response = isLogin
                ? await authApi.login({ email, password })
                : await authApi.register({ email, password, firstName, lastName });

            login(response.data);
            navigate('/tasks');
        } catch (err) {
            setToast({ message: isLogin ? t('authError') : t('createAccountError'), type: "error" });
        }
    };

    return (
        <div className="login-page">
            <Button label={language === 'fr' ? 'EN' : 'FR'} mode="secondary" onClick={toggle} className="lang-toggle" />
            <div className="login-container card">
                <h1>{t('appName')}</h1>
                <h2>{isLogin ? t('login') : t('register')}</h2>
                <p className="login-subtitle">{t('loginSubtitle')}</p>
                <form>
                    {!isLogin && (
                        <>
                            <div>
                                <Input
                                    label={t('firstName')}
                                    type='text'
                                    id='firstName'
                                    value={firstName}
                                    onChange={(e) => setFirstName(e.target.value)}
                                />
                            </div>
                            <div>
                                <Input
                                    label={t('lastName')}
                                    type='text'
                                    id='lastName'
                                    value={lastName}
                                    onChange={(e) => setLastName(e.target.value)}
                                />
                            </div>
                        </>
                    )}
                    <div>
                        <Input
                            label={t('email')}
                            type='email'
                            id='email'
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            isInvalid={errors.email}
                        />
                    </div>
                    <div>
                        <Input
                            label={t('password')}
                            type='password'
                            id='password'
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            isInvalid={errors.password}
                        />
                    </div>
                    <Button type="submit" label={isLogin ? t('login') : t('register')} onClick={handleSubmit} />
                </form>
                <div className="login-footer">
                    <span>{isLogin ? t('noAccount') : t('alreadyAccount')}</span>
                    <Button
                        type='button'
                        onClick={() => setIsLogin(!isLogin)}
                        label={isLogin ? t('register') : t('login')}
                        mode="light"
                    />
                </div>
            </div>
            <Toast message={toast?.message} type={toast?.type} onClose={() => setToast(null)} />
        </div>
    );
}