<?php
  $imei     = (empty($_GET['imei'])) ? '' : $_GET['imei'];
  $username = (empty($_GET['username'])) ? '' : $_GET['username'];
//  $password = (empty($_GET['password'])) ? '' : $_GET['password'];
  $email    = (empty($_GET['email'])) ? '' : $_GET['email'];
  $ok = true;

  if ($imei == '')
  {
    echo "Parâmetro 'imei' não foi informado.";
  }
  elseif ($username == '')
  {
    echo "Parâmetro 'username' não foi informado.";
  }
//  elseif ($password == '')
//  {
//    echo "Parâmetro 'password' não foi informado.";
//  }
  elseif ($email == '')
  {
    echo "Parâmetro 'email' não foi informado.";
  }
  else
  {
    // Generate a random password
    srand((double)microtime()*10000000);
    $password = md5 (dechex(rand()));
    
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
    
      $sql = "select id from users where email='$email'";
      $result = mysql_query($sql, $link);
      $user = "";

      if (!$result) 
      {
          echo 'ERROR: ' . mysql_error();
          $ok = false;
      }
      else
      {
        while ($row = mysql_fetch_assoc($result)) 
        {
          $user = $row['id'];
        }
      }        
      mysql_free_result($result);        

      if ($user = "")
      {
        $ok = false;
        echo "e-mail não encontrado para nenhum usuário.";
      }

      if ($ok)
      {
        $sql = "INSERT INTO devices (imei, added, username, password, user) " .
               "VALUES ('$imei', current_timestamp, '$username', '$password', $user)";
               
        $result = mysql_query($sql, $link);

        if (!$result) 
        {
            echo 'ERROR: ' . mysql_error();
            $ok = false;
        }
        else
        {
          mysql_free_result($result);
                  
          $sql = "select id from devices where imei='$imei'";
          $result = mysql_query($sql, $link);

          if (!$result) 
          {
              echo 'ERROR: ' . mysql_error();
              $ok = false;
          }
          else
          {
            while ($row = mysql_fetch_assoc($result)) 
            {
              $id = $row['id'];
            }
            $v = base64_encode($password);
            echo "adicione esse link no google earth: http://joubertvasc.dnsalias.net:8080/getposition.php?id=$id&v=$v";
          }        
        } /**/
      } /**/ 
    }
    
    mysql_free_result($result); /**/
    mysql_close($link);
  }
?>
