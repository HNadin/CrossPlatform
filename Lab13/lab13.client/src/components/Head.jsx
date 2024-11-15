import { Helmet } from 'react-helmet';

const Head = () => {
    return (
        <Helmet>
            <meta charSet="UTF-8" />
            <title>Ласкаво просимо до веб-застосунку!</title>
            <meta name="description" content="Цей застосунок пропонує можливості реєстрації, авторизації, та виконання практичних робіт." />
            <meta name="viewport" content="width=device-width, initial-scale=1" />
        </Helmet>
    );
};

export default Head;
