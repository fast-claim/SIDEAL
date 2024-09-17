<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head> 
<body>
<?php



require '../../../vendor/autoload.php';
use Dotenv\Dotenv;

// Cargar las variables de entorno desde el archivo .env

$dotenv = new Dotenv(__DIR__ , "/../../../");
$dotenv->load();
function OpenConnection()
{
    // Obtener variables de entorno para la conexión a la base de datos
    $serverName = "tcp:" . (string) getenv('DB_SERVER'); // Forzar el uso de TCP/IP con prefijo 'tcp:'
    $database = (string) getenv('DB_DATABASE');
    $username = (string) getenv('DB_USERNAME');
    $password = (string) getenv('DB_PASSWORD');

    echo "Intentando conectar a SQL Server en $serverName con la base de datos $database usando el usuario $username.<br>";

    // Configurar las opciones de conexión
    $connectionOptions = array(
        "Database" => $database,
        "Uid" => $username,
        "PWD" => $password
    );

    // Conectar a la base de datos usando sqlsrv_connect
    $conn = sqlsrv_connect($serverName, $connectionOptions);

    // Verificar si la conexión fue exitosa
    if ($conn === false) {
        die(print_r(sqlsrv_errors(), true)); // Mostrar el error si la conexión falla
    }

    echo "Conexión exitosa"; // Mensaje de éxito

    return $conn; // Devolver la conexión para ser usada fuera de la función
}

// Llamar a la función para abrir la conexión
$conn = OpenConnection();
?>

</body>
</html>

