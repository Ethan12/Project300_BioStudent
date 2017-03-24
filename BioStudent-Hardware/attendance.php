<?php
if(isset($_POST)){
$connectionInfo = array("UID" => "biostudent@jeef", "pwd" => "fingerprint123**", "Database" => "jeefdb", "LoginTimeout" => 30, "Encrypt" => 1, "TrustServerCertificate" => 0);
$serverName = "tcp:jeef.database.windows.net,1433";
$conn = sqlsrv_connect($serverName, $connectionInfo);

//$userID = (int)$_POST["userID"];
//$moduleID = (int)$_POST["moduleID"];

$post = file_get_contents('php://input');
$data = json_decode($post);
$userID = $data->userID;
$moduleID = $data->moduleID;

if($conn)
{
  $selsql = "SELECT [Id] FROM dbo.StudentUserAccount WHERE FingerprintID=$userID";

  $getUser = sqlsrv_query($conn, $selsql);
if ($getUser == FALSE){
    die("cannot get user");
  }
  $userdbID = "";
while($row = sqlsrv_fetch_array($getUser, SQLSRV_FETCH_ASSOC))
{
    $userdbID += $row['Id'];
}
sqlsrv_free_stmt($getUser);
$usr = (int)$userdbID;
echo "USERID: " + $usr;
flush();
  $tsql = "INSERT dbo.StudentAttendance (StudentId, ModuleId, Attended, Date) OUTPUT Inserted.Id VALUES($usr, $moduleID, 1, getdate())";

  //Insert Query
  $insert_attendance = sqlsrv_query($conn, $tsql);
  if($insert_attendance == FALSE)
  {
    echo "error";
    flush();
  }else{
    echo "{StudentAttendanceID=";
      while($row = sqlsrv_fetch_array($insert_attendance, SQLSRV_FETCH_ASSOC))
      {
        echo($row['Id']);
        flush();
      }
      echo "}";
      flush();
      sqlsrv_free_stmt($insert_attendance);
      sqlsrv_close($conn);
  }

}else{
  echo "Error";
  flush();
}
}
?>
