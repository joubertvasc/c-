<?php
// http://127.0.0.1:8080/getposition.php?id=1&v=7ea597b6cc98823feaa24d604482c34e
  $deviceid   = (empty($_GET['id'])) ? '' : $_GET['id'];
  $devicepass = (empty($_GET['v'])) ? '' : $_GET['v'];
  $x="?";

  if ($deviceid != "" && $devicepass != "")
  {
    $link = mysql_connect('localhost', 'jvsoftware', '45ma12mb');
    if (!$link) {
        echo 'Cannot connect to database server: ' . mysql_error();
        exit;
    }

    if (!mysql_select_db('mt', $link)) {
        echo 'Cannot select database';
        exit;
    }

    $sql = "SELECT D.manufacturer, D.model, D.password, D.username, C.date, " .
           "       C.satelittes, C.speed, C.altitude, C.latitude, C.longitude, " .
           "       case when type='G' then 'GPS' else case when type= 'O' then 'OpenCellID' else 'CellDB' end end as CC_type" .
           "  FROM devices D, users U, coordinates C " .
           " WHERE D.id=$deviceid" .
           "   AND D.disabled is null" .
           "   AND D.user=U.id" .
           "   AND U.inuse = 'Y'" .
           "   AND C.device = D.id" .
           "   AND C.last = 'Y'";
    $result = mysql_query($sql, $link);

    if (!$result) {
        echo 'Error : ' . mysql_error();
        exit;
    }

    if (mysql_num_rows($result) > 0) {
      while ($row = mysql_fetch_assoc($result)) {
        if ($row['password'] == $devicepass) {
          $model=$row['manufacturer'] . "-" . $row['model'] . 
                 " User: " . $row['username'] . " - date: " . $row['date'] .
                 " Speed: " . $row['speed'] . " Altitude: " . $row['altitude'];
          $lo=$row['longitude'];
          $la=$row['latitude'];
        }
        else
        {
          $model="Invalid URL";
        }
      }
    } 
    else
    {
      $model="ID or coordinate not found";
    }  

    mysql_free_result($result);
    mysql_close($link);
  }
  else
  {
    $model="Invalid URL";
  }
  
  echo "<?xml version=\"1.0\" encoding=\"UTF-8\" $x>
        <kml xmlns=\"http://www.opengis.net/kml/2.2\">
          <Placemark>
            <name>$model</name>
            <Point>
              <coordinates>$lo,$la</coordinates>
            </Point>
          </Placemark>
        </kml>";
?>
