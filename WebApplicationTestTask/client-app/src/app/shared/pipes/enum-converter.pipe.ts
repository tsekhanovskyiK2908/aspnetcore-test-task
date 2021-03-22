import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumConverter'
})
export class EnumConverterPipe implements PipeTransform {

  transform(object: Object): Array<{key: string, value: any}> {
    const keys = Object.keys(object);
    return keys.map(key => ({ key, value: Object(object)[key] }) );
  }
}
