namespace Edunite.Application.Features.Teacher.GetTeacherDetailsById;

public class GetTeacherDetailsByIdQueryHandler : IRequestHandler<GetTeacherDetailsByIdQuery, TeacherDetailDto>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITeacherCertificateRepository _certificateRepository;
    private readonly IByteAndFormConverterExtension _converter;
    public GetTeacherDetailsByIdQueryHandler(ITeacherCertificateRepository certificateRepository,
        ITeacherRepository teacherRepository, IByteAndFormConverterExtension converter)
    {
        _teacherRepository = teacherRepository;
        _certificateRepository = certificateRepository;
        _converter = converter;
    }

    #region Handle

    public async Task<TeacherDetailDto> Handle(GetTeacherDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var teacherDetail = await _teacherRepository.GetTeacherByUserIdAsync(request.UserId);
        if (teacherDetail == null)
        {
            throw new Exception("No Teacher Assign to this id");
        }
        var teacherDetailId = teacherDetail.TeacherDetailId;
        var teacherCerti = await _certificateRepository.GetTeacherCertificateByIdAsync(teacherDetailId);
        if (teacherCerti == null)
        {
            throw new Exception("Form can't convert");
        }
        //left to change byte[] to iformfile
        var fileByte = teacherCerti.Certificate;
        if (fileByte == null)
        {
            throw new Exception("Teacher does not have photo");
        }
        var form = _converter.ConvertByteArrayToIFormFile(fileByte, "photo", "image/jpg");
        return new TeacherDetailDto
        {
            TeacherDetailId = teacherDetailId,
            TeacherCertificateId = teacherCerti.TeacherCertificateId,
            Certificate = form
        };
    }

	#endregion
}
