<?php

use App\Http\Controllers\Api\FuncionarioController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\Api\V1\BookController as BookV1;
use App\Http\Resources\FuncionarioResource;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});

Route::apiResource('v1/books', BookV1::class)
      ->only(['index','show', 'destroy'])
      /*->middleware('auth:sanctum')*/;
Route::apiResource('funcionarios', FuncionarioController::class)
->only(['index','show', 'destroy']);

Route::get('/obtener-muestra-pantalla',[FuncionarioController::class,'obtenerMuestraPantalla']);
Route::put('/actualizar-bloque-mostrado/{id}',[FuncionarioController::class,'actualizarBloqueMostrado']);
Route::put('/actualizar-bloque-a-mostrar/{id_funcionario}',[FuncionarioController::class,'actualizarBloqueAMostar']);
