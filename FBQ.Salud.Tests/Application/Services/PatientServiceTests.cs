using Moq;
using FBQ.Salud_Domain.Dtos;
using FBQ.Salud_Domain.Commands;
using FBQ.Salud_Application.Services;
using FBQ.Salud_Domain.Entities;
using AutoMapper;
using FBQ.Salud_Application.Validation;

namespace FBQ.Salud.Tests.Application.Services
{
    public class PacienteServiceTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IPacienteRepository> _mockRepository;
        private readonly Mock<IPacienteValidationExist> _mockValidation;
        private readonly PacienteServices _service;

        public PacienteServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IPacienteRepository>();
            _mockValidation = new Mock<IPacienteValidationExist>();

            _service = new PacienteServices(_mockMapper.Object, _mockRepository.Object, _mockValidation.Object);
        }

        [Fact]
        public void Add_ShouldReturnSuccessResponse_WhenValidPacienteDtoProvided()
        {
            // Arrange
            var pacienteDto = new PacienteDto
            {
                Nombre = "Claudio",
                Apellido = "Damico",
                Edad = 35,
                Sexo = "Masculino",
                DNI = "12345678",
                Direccion = "Calle Falsa",
                DirecionNumero = "123",
                Telefono = "555-1234",
                Foto = "foto.jpg"
            };

            _mockValidation.Setup(v => v.ExistePaciente(It.IsAny<Paciente>())).Returns(true);
            _mockMapper.Setup(m => m.Map<Paciente>(It.IsAny<PacienteDto>())).Returns(new Paciente());

            // Act
            var response = _service.Add(pacienteDto);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Éxito", response.Message);
        }

        [Fact]
        public void Delete_ShouldReturnError_WhenPacienteDoesNotExist()
        {
            // Arrange
            int pacienteId = 99; // ID inexistente
            _mockRepository.Setup(r => r.GetPacienteById(pacienteId)).Returns((Paciente)null);

            // Act
            var response = _service.Delete(pacienteId);

            // Assert
            Assert.False(response.Success);
            Assert.Equal($"Empleado con id {pacienteId} inexistente", response.Message);
            _mockRepository.Verify(r => r.Update(It.IsAny<Paciente>()), Times.Never); // Verificar que no se actualizó
        }

        [Fact]
        public void GetPacienteByDni_ShouldReturnPaciente_WhenPacienteExists()
        {
            // Arrange
            string dni = "12345678";
            var existingPaciente = new Paciente
            {
                PacienteId = 1,
                Nombre = "Claudio",
                Apellido = "Damico",
                DNI = dni,
                Estado = true
            };

            _mockRepository.Setup(r => r.GetPacienteByDNI(dni)).Returns(existingPaciente);
            _mockMapper.Setup(m => m.Map<PacienteDto>(existingPaciente)).Returns(new PacienteDto
            {
                Nombre = "Claudio",
                Apellido = "Damico",
                DNI = dni
            });

            // Act
            var response = _service.GetPacienteByDni(dni);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Éxito", response.Message); // Cambiado de "Exito" a "Éxito"
            Assert.NotNull(response.Result);
            Assert.IsType<PacienteDto>(response.Result);
            _mockRepository.Verify(r => r.GetPacienteByDNI(dni), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdatePaciente_WhenPacienteExists()
        {
            // Arrange
            int pacienteId = 1;
            var pacienteDto = new PacienteDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                DNI = "12345678",
                Edad = 35
            };

            var pacienteExistente = new Paciente
            {
                PacienteId = pacienteId,
                Nombre = "Juan",
                Apellido = "Perez",
                DNI = "12345678",
                Edad = 30
            };

            _mockRepository.Setup(r => r.GetPacienteById(pacienteId)).Returns(pacienteExistente);
            _mockValidation.Setup(v => v.ExistePaciente(It.IsAny<Paciente>())).Returns(true);

            // Act
            var response = _service.Update(pacienteId, pacienteDto);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Paciente modificado", response.Message);
            _mockRepository.Verify(r => r.Update(It.IsAny<Paciente>()), Times.Once);
        }

        [Fact]
        public void Update_ShouldReturnError_WhenPacienteDoesNotExist()
        {
            // Arrange
            int pacienteId = 99; // ID inexistente
            var pacienteDto = new PacienteDto
            {
                Nombre = "Nuevo",
                Apellido = "Paciente",
                Edad = 30,
                DNI = "98765432"
            };

            _mockRepository.Setup(r => r.GetPacienteById(pacienteId)).Returns((Paciente)null);

            // Act
            var response = _service.Update(pacienteId, pacienteDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal($"Paciente con id {pacienteId} inexistente", response.Message);
            _mockRepository.Verify(r => r.Update(It.IsAny<Paciente>()), Times.Never);
        }

        [Fact]
        public void GetAll_ShouldReturnListOfPacientes_WhenPacientesExist()
        {
            // Arrange
            var pacientes = new List<Paciente>
    {
        new Paciente { PacienteId = 1, Nombre = "Claudio", Apellido = "Damico", DNI = "12345678" },
        new Paciente { PacienteId = 2, Nombre = "Ana", Apellido = "Perez", DNI = "87654321" }
    };

            _mockRepository.Setup(r => r.GetListPacientesByNombre()).Returns(pacientes);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockRepository.Verify(r => r.GetListPacientesByNombre(), Times.Once);
        }

        [Fact]
        public void Add_ShouldReactivatePaciente_WhenPacienteExistsAndIsInactive()
        {
            // Arrange
            var pacienteDto = new PacienteDto
            {
                Nombre = "Claudio",
                Apellido = "Damico",
                DNI = "12345678"
            };

            var existingPaciente = new Paciente
            {
                PacienteId = 1,
                Nombre = "Claudio",
                Apellido = "Damico",
                DNI = "12345678",
                Estado = false // Inactivo
            };

            _mockMapper.Setup(m => m.Map<Paciente>(It.IsAny<PacienteDto>())).Returns(existingPaciente);
            _mockValidation.Setup(v => v.ExistePaciente(It.IsAny<Paciente>())).Returns(false);
            _mockRepository.Setup(r => r.GetPacienteByDNI(existingPaciente.DNI)).Returns(existingPaciente);
            _mockRepository.Setup(r => r.Update(existingPaciente)).Callback(() => existingPaciente.Estado = true);

            // Act
            var response = _service.Add(pacienteDto);

            // Assert
            Assert.True(response.Success);
            Assert.Equal($"Paciente con DNI {pacienteDto.DNI} activado", response.Message);
            _mockRepository.Verify(r => r.Update(It.IsAny<Paciente>()), Times.Once);
        }

        [Fact]
        public void GetAll_ShouldReturnAllPacientes()
        {
            // Arrange
            var pacientes = new List<Paciente>
    {
        new Paciente { PacienteId = 1, Nombre = "Juan", Apellido = "Perez" },
        new Paciente { PacienteId = 2, Nombre = "Ana", Apellido = "Lopez" }
    };

            _mockRepository.Setup(r => r.GetListPacientesByNombre()).Returns(pacientes);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Juan", result[0].Nombre);
            Assert.Equal("Ana", result[1].Nombre);
        }

        [Fact]
        public void Add_ShouldThrowArgumentNullException_WhenPacienteDtoIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.Add(null));
        }

        [Fact]
        public void GetPacienteByDni_ShouldReturnError_WhenDniIsEmpty()
        {
            // Arrange
            string dni = "";

            // Act
            var response = _service.GetPacienteByDni(dni);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("El DNI no puede estar vacío.", response.Message);
            _mockRepository.Verify(r => r.GetPacienteByDNI(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Add_ShouldNotAddPaciente_WhenValidationFails()
        {
            // Arrange
            var pacienteDto = new PacienteDto
            {
                Nombre = "Test",
                Apellido = "User",
                DNI = "12345678"
            };

            var paciente = new Paciente
            {
                Nombre = "Test",
                Apellido = "User",
                DNI = "12345678"
            };

            _mockMapper.Setup(m => m.Map<Paciente>(pacienteDto)).Returns(paciente);
            _mockValidation.Setup(v => v.ExistePaciente(paciente)).Returns(false);

            // Act
            var response = _service.Add(pacienteDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal($"Existe un paciente con DNI {pacienteDto.DNI}", response.Message);
            _mockRepository.Verify(r => r.Add(It.IsAny<Paciente>()), Times.Never);
        }
    }
}



