namespace AuthenticationServiceNamespace
{
    public class AuthenticationService
    {
        private readonly IProfileDao _profileDao;
        private readonly IHash _sha256Adapter;
        private readonly IOtp _otpService;

        public AuthenticationService(IProfileDao profileDao, IHash sha256Adapter, IOtp otpService)
        {
            _profileDao = profileDao;
            _sha256Adapter = sha256Adapter;
            _otpService = otpService;
        }

        public bool Verify(string accountId, string password, string otp)
        {
            var passwordFromDb = _profileDao.GetPassword(accountId);
            var hashedPassword = _sha256Adapter.Compute(password);
            var currentOtp = _otpService.GetCurrentOtp();

            return passwordFromDb == hashedPassword && otp == currentOtp;
        }
    }
}