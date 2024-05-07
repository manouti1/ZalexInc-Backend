using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ZalexInc.Certification.API.Controllers;
using ZalexInc.Certification.Application.Commands;
using ZalexInc.Certification.Application.Dtos;
using ZalexInc.Certification.Application.Helpers;
using ZalexInc.Certification.Application.Queries;
using ZalexInc.Certification.Domain.Utils;

namespace ZalexInc.Certification.Tests
{
    public class CertificatesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CertificatesController _controller;

        public CertificatesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CertificatesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateCertificate_ReturnsOk_WhenCommandIsSuccessful()
        {
            var command = new CreateCertificateCommand();
            var dto = new CertificateDto();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCertificateCommand>(), default))
                         .ReturnsAsync(OperationResult<CertificateDto>.Ok(dto));

            var result = await _controller.CreateCertificate(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, okResult.Value);
        }


        [Fact]
        public async Task UpdateCertificate_ReturnsBadRequest_WhenIdMismatch()
        {
            var command = new UpdateCertificateCommand { Id = Guid.NewGuid() };

            var result = await _controller.UpdateCertificate(Guid.NewGuid(), command);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCertificate_ReturnsOk_WhenDeletionIsSuccessful()
        {
            var command = new DeleteCertificateCommand { Id = Guid.NewGuid() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCertificateCommand>(), default))
                         .ReturnsAsync(OperationResult.Ok());

            var result = await _controller.DeleteCertificate(command.Id);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetCertificateById_ReturnsOk_WhenFound()
        {
            var id = Guid.NewGuid();
            var certificateDto = new CertificateDto();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCertificateByIdQuery>(), default))
                         .ReturnsAsync(OperationResult<CertificateDto>.Ok(certificateDto));

            var result = await _controller.GetCertificateById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(certificateDto, okResult.Value);
        }

        [Fact]
        public async Task GetCertificateById_ReturnsNotFound_WhenCertificateDoesNotExist()
        {
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCertificateByIdQuery>(), default))
                         .ReturnsAsync(OperationResult<CertificateDto>.Fail("Not Found"));

            var result = await _controller.GetCertificateById(id);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCertificates_ReturnsOk_WithPagination()
        {
            var query = new GetPaginatedCertificatesQuery(1, 10, "IssuedOn");
            var pagination = new Pagination<CertificateDto>(1, 10, 0, new List<CertificateDto>());
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPaginatedCertificatesQuery>(), default))
                         .ReturnsAsync(OperationResult<Pagination<CertificateDto>>.Ok(pagination));

            var result = await _controller.GetCertificates(query.PageIndex, query.PageSize, query.SortBy);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pagination, okResult.Value);
        }
    }
}
