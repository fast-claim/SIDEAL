
<?php

require '../../../vendor/autoload.php';
use Dotenv\Dotenv;

// Cargar las variables de entorno desde el archivo .env utilizando el método correcto
$dotenv = Dotenv::createImmutable(__DIR__ );
$dotenv->load();

function OpenConnection()
{
    // Obtener variables de entorno para la conexión a la base de datos
    $serverName = $_ENV['DB_SERVER'];
    $database = $_ENV['DB_DATABASE'];
    $username = $_ENV['DB_USERNAME'];
    $password = $_ENV['DB_PASSWORD'];


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

$sql = "SELECT * FROM articulos";
$stmt = sqlsrv_query($conn, $sql);

if ($stmt === false) {
    http_response_code(500);
    die(json_encode(array("error" => sqlsrv_errors())));
}

$data = array();
while ($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
    $data[] = $row;
}

// Devolver los datos en formato JSON
header('Content-Type: application/json');
echo json_encode($data);

// Cerrar la conexión
sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);
?>

