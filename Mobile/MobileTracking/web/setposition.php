<?php
// http://127.0.0.1:8080/setposition.php?id=MzU2MDU2MDEwMDU0MTQ0&la=-27&lo=-45&t=G&s=60&sc=4&a=15
  $imei       = (empty($_GET['id'])) ? '' : $_GET['id'];
  $la         = (empty($_GET['la'])) ? '' : $_GET['la'];
  $lo         = (empty($_GET['lo'])) ? '' : $_GET['lo'];
  $speed      = (empty($_GET['s']))  ? '' : $_GET['s'];
  $altitude   = (empty($_GET['a']))  ? '' : $_GET['a'];
  $satelittes = (empty($_GET['sc'])) ? '' : $_GET['sc'];
  $type       = (empty($_GET['t']))  ? '' : $_GET['t']; 

  if ($imei == '')
  {
    echo "INVALID ID";
  }
  elseif ($la == '' || $lo == '')
  {
    echo "INVALID COORDINATES";
  }
  elseif ($type == '')
  {
    echo "INVALID TYPE";
  }
  else  
  {
    $imei=base64_decode($imei);
    $ok = true;
    
    $link = mysql_connect('localhost', 'jvsoftware', '45ma12mb');
    if (!$link) {
        echo 'ERROR connecting to database server: ' . mysql_error();
        $ok = false;
    }

    if ($ok)
    {
      if (!mysql_select_db('mt', $link)) 
      {
          echo 'ERROR selecting database';
          $ok = false;
      }
    
      if ($ok)
      {
        $sql = "SELECT D.id FROM devices D, users U " .
               " WHERE D.imei=$imei" .
               "   AND D.disabled is null" .
               "   AND D.user=U.id" .
               "   AND U.inuse = 'Y'";
        $result = mysql_query($sql, $link);

        if (!$result) 
        {
            echo 'ERROR: ' . mysql_error();
            $ok = false;
        }
    
        if ($ok)
        {
          if (mysql_num_rows($result) == 0) 
          {
            echo "ERROR INVALID DEVICE OR USER DISABLED";
            $ok = false;
          }
          else  
          {
            while ($row = mysql_fetch_assoc($result)) 
            {
              $id = $row['id'];
            }
          }
        }  
      } 
    }
    
    mysql_free_result($result);
    
    if ($ok)
    {
      $sql = "SET AUTOCOMMIT=0";
      $result = mysql_query($sql, $link);

      if (!$result) 
      {
          echo 'ERROR: ' . mysql_error();
          $ok = false;
      }
      mysql_free_result($result);

      if ($ok)
      {
        $sql = "UPDATE coordinates set last = 'N' where device=$id and last = 'Y'";
        $result = mysql_query($sql, $link);

        if (!$result) 
        {
          echo 'ERROR: ' . mysql_error();
          $ok = false;
        }
        mysql_free_result($result);
        
        if ($ok)
        {
          $sql = "INSERT INTO coordinates (device, last, satelittes, speed, altitude, " .
                 "                         latitude, longitude, type, date) " .
                 "VALUES ($id, 'Y', " .
                          ($satelittes == '' ? "null" : "'$satelittes'") . ", " .
                          ($speed == '' ? "null" : "'$speed'") . ", " .
                          ($altitude == '' ? "null" : "'$altitude'") . ", " .
                 "        '$la', '$lo', '$type', current_timestamp)";
                 
          $result = mysql_query($sql, $link);

          if (!$result) 
          {
            echo 'ERROR: ' . mysql_error();
            $ok = false;
          }
          mysql_free_result($result);
          
          if ($ok)
          {
            $sql = "commit";
            echo "OK";
          }
          else
          {
            $sql = "rollback";
          }

          $result = mysql_query($sql, $link);
          mysql_free_result($result);
        }
      }
    }
    
    mysql_close($link);
  } /**/
?>
