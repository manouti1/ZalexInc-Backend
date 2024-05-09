using AutoMapper;
using FluentValidation;
using MediatR;
using ZalexInc.Certification.Application.Commands;
using ZalexInc.Certification.Application.Dtos;
using ZalexInc.Certification.Application.Helpers;
using ZalexInc.Certification.Application.Queries;
using ZalexInc.Certification.Domain.Entities;
using ZalexInc.Certification.Domain.Interfaces;
using ZalexInc.Certification.Domain.Utils;

namespace ZalexInc.Certification.Application.Handlers
{
    public class CertificateHandler :
        IRequestHandler<CreateCertificateCommand, OperationResult<CertificateDto>>,
        IRequestHandler<UpdateCertificateCommand, OperationResult>,
        IRequestHandler<DeleteCertificateCommand, OperationResult>,
        IRequestHandler<GetCertificateByIdQuery, OperationResult<CertificateDto>>,
        IRequestHandler<GetPaginatedCertificatesQuery, OperationResult<Pagination<CertificateDto>>>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCertificateCommand> _createValidator;
        private readonly IValidator<UpdateCertificateCommand> _updateValidator;


        public CertificateHandler(ICertificateRepository certificateRepository,
                                  IMapper mapper,
                                  IValidator<CreateCertificateCommand> createValidator,
                                  IValidator<UpdateCertificateCommand> updateValidator)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<OperationResult<CertificateDto>> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _createValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    return OperationResult<CertificateDto>.Fail("Validation failed", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

                var certificate = _mapper.Map<Certificate>(request);
                var result = await _certificateRepository.CreateCertificateAsync(certificate);
                return OperationResult<CertificateDto>.Ok(_mapper.Map<CertificateDto>(result));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<OperationResult> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _updateValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    return OperationResult.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

                var certificate = await _certificateRepository.GetAllCertificateByIdAsync(request.Id);
                if (certificate == null)
                    return OperationResult.Fail("Certificate not found");

                _mapper.Map(request, certificate);
                await _certificateRepository.UpdateCertificateAsync(certificate);
                return OperationResult.Ok();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<OperationResult> Handle(DeleteCertificateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _certificateRepository.DeleteCertificateAsync(request.Id);
                return result ? OperationResult.Ok() : OperationResult.Fail("Certificate not found");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<OperationResult<CertificateDto>> Handle(GetCertificateByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var certificate = await _certificateRepository.GetAllCertificateByIdAsync(request.Id);

                var certificateDto = _mapper.Map<CertificateDto>(certificate);

                if (certificate == null)
                    return OperationResult<CertificateDto>.Fail("Certificate not found", certificateDto);

                return OperationResult<CertificateDto>.Ok(_mapper.Map<CertificateDto>(certificate));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<OperationResult<Pagination<CertificateDto>>> Handle(GetPaginatedCertificatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var certificates = await _certificateRepository.GetCertificatesPaginatedAsync(request.PageIndex, request.PageSize, request.SortBy);
                var certificateDtos = _mapper.Map<List<CertificateDto>>(certificates);
                var totalCount = await _certificateRepository.GetTotalCertificatesCountAsync();

                var pagination = new Pagination<CertificateDto>(request.PageIndex, request.PageSize, totalCount, certificateDtos);
                return OperationResult<Pagination<CertificateDto>>.Ok(pagination);
            }
            catch (Exception e) { throw; }
        }
    }
}
