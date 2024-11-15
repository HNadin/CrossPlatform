import PropTypes from 'prop-types';

const ErrorPage = ({ requestId }) => {
    return (
        <div className="container mt-5">
            <h1 className="text-danger">Error</h1>
            <h2 className="text-danger">An error occurred while processing your request.</h2>
            {requestId && (
                <p>
                    <strong>Request ID:</strong> <code>{requestId}</code>
                </p>
            )}
            <h3>Development Mode</h3>
            <p>
                Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
            </p>
            <p>
                <strong>The Development environment shouldn&apost be enabled for deployed applications.</strong>
                It can result in displaying sensitive information from exceptions to end users.
                For local debugging, enable the <strong>Development</strong> environment by setting the
                <strong> ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong> and restarting the app.
            </p>
        </div>
    );
};

ErrorPage.propTypes = {
    requestId: PropTypes.string,
};

export default ErrorPage;
