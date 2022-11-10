<?php

namespace App\Http\Resources;

use Illuminate\Http\Resources\Json\JsonResource;

class FuncionarioResource extends JsonResource
{
    /**
     * Transform the resource into an array.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return array|\Illuminate\Contracts\Support\Arrayable|\JsonSerializable
     */
    public function toArray($request)
    {
      //  return parent::toArray($request);
      return [
        'id' => $this->id,
        'id_funcionario' => $this->id_funcionario,
        'nombre_funcionario' => $this->nombre_funcionario,
        'muestra_pantalla' => $this->muestra_pantalla
       ];
    }
}
