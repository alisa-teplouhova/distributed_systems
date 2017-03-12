<?php
require_once 'include/common.inc.php';

if (isPost())
{
    $corrId = getPostParam(PARAMETER_CORRID);
    $poemInfo = getPoemInfo($corrId);
    $result = [
        'result' => !$poemInfo['Error'],
        'poem' => isset($poemInfo['Poem']) ? $poemInfo['Poem'] : ""
    ];
    echo json_encode($result);
}