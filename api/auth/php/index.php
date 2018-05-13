<?php
 // using composer https://getcomposer.org/
 require __DIR__ . '/vendor/autoload.php';
 // JWT PHP Library https://github.com/firebase/php-jwt
 use \Firebase\JWT\JWT;

function generateToken($content) {
    $API_KEY = 'PUT YOUR API KEY HERE';
    $API_SECRET = 'PUT YOUR API SECRET HERE';

    $payload = array(
        "jti" => getGUID(),
        "iss" => $API_KEY,
        "iat" => time(),
        "sub" => hash('sha256', $content),
        "exp" => time() + 10    // 10 seconds expiration
    );
    return JWT::encode($payload, $secret);
}

function getGUID(){
    if (function_exists('com_create_guid')){
        return com_create_guid();
    }else{
        $charid = strtoupper(md5(uniqid(rand(), true)));
        $hyphen = chr(45);
        $uuid = chr(123)
            .substr($charid, 0, 8).$hyphen
            .substr($charid, 8, 4).$hyphen
            .substr($charid,12, 4).$hyphen
            .substr($charid,16, 4).$hyphen
            .substr($charid,20,12)
            .chr(125);
        return $uuid;
    }
}

function generateInterview () {
    $ch = curl_init('https://interviews-int.skype.com/api/interviews');
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_POSTFIELDS, '{}');
    curl_setopt($ch, CURLOPT_VERBOSE, true);

    curl_setopt($ch, CURLOPT_HTTPHEADER, array(
        'Authorization: Bearer ' . generateToken('{}'),
        'Content-Type: application/json'
    ));
    $response = curl_exec($ch);
    var_dump($response);
    $response = json_decode($response);

    curl_close($ch);
    return $response;
}

var_dump(generateInterview());
?>