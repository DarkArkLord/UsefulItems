const tableOffset = '    '
const tableBorder = {
    start: {
        border: tableOffset + '+-',
        line: tableOffset + '| ',
    },
    end: {
        border: '-+',
        line: ' |',
    },
}
const headers = ['Столбец', 'Тип данных', 'Описание',];
const inputData = [
    {
        header: '"factReception" - содержит данные о приемах в частной клинике каждого пациента у каждого врача с учетом каждой оказанной услуги.',
        lines: [
            ['keyReception ', 'int not null', 'Поле первичного ключа. Идентификационный номер приема',],
            ['keyServiceInReception ', 'int not null', 'Поле первичного ключа. Идентификационный номер оказаннй во время приема услуги',],
            ['DoctorKey', 'int not null', 'Код доктора, оказывающего услугу. Внешний ключ к таблице измерения "dimDoctor"',],
            ['ServiceKey', 'int not null', 'Код оказываемой услуги. Внешний ключ к таблице измерения "dimService"',],
            ['ServiceDuration', 'int not null', 'Время оказания услуги в минутах',],
            ['BranchKey', 'int not null', 'Код филиала. Внешний ключ к таблице измерения "dimBranch"',],
            ['DateKey', 'int not null', 'Ссылка на дату оказания услуги. Внешний ключ к таблице измерения "dimDate"',],
            ['ClientKey', 'int not null', 'Номер пациента. Внешний ключ к таблице измерения "dimClient"',],
            ['ClientInfoKey', 'int not null', 'Код текущих данных по пациенту. Внешний ключ к таблице измерения "miniDimClientInfo"',],
            ['Price', 'int not null', 'Полная цена оказанной услуги',],
            ['Discont', 'int not null', 'Размер скидки',],
            ['TotalPrice', 'int not null', 'Итоговая цена',],
        ]
    },
    {
        header: '"dimDoctor" - данные о докторах.',
        lines: [
            ['keyDoctor', 'int not null', 'Суррогатный первичный ключ со свойством IDENTITY(1, 1)',],
            ['altKeyDoctor', 'int not null', 'Бизнес ключ доктора из транзакционной бд',],
            ['Speciality', 'varchar not null', 'Специальность доктора. SCD тип 2',],
            ['IsActual', 'bool not null', 'Отметка об актуальность записи',],
        ]
    },
    {
        header: '"dimService" - данные об оказываемых услугах.',
        lines: [
            ['keyService', 'int not null', 'Суррогатный первичный ключ со свойством IDENTITY(1, 1)',],
            ['Kind', 'varchar not null', 'Название услуги',],
        ]
    },
    {
        header: '"dimBranch" - данные о филиале оказания услуг.',
        lines: [
            ['keyBranch', 'int not null', 'Суррогатный первичный ключ со свойством IDENTITY(1, 1)',],
            ['altKeyBranch', 'int not null', 'Бизнес ключ филиала из транзакционной бд',],
            ['CountryName', 'varchar not null', 'Название страны',],
            ['CityName', 'varchar not null', 'Название города',],
            ['CityRegionName', 'varchar null', 'Название городского региона',],
        ]
    },
    {
        header: '"dimDate" - содержит временную шкалу с грануляцией по дням. Заполняется при помощи SQL скрипта.',
        lines: [
            ['keyDate', 'bigint not null', 'Первичный ключ, являющийся производным от временного знаечния',],
            ['Year', 'int not null', 'Год',],
            ['Quartal', 'int not null', 'Квартал',],
            ['Month', 'int not null', 'Месяц',],
            ['Day', 'int not null', 'День',],
            ['WeekDay', 'int not null', 'Номер дня в неделе',],
            ['MonthName', 'varchar not null', 'Название месяца',],
            ['WeekDayName', 'varchar not null', 'Название дня в неделе',],
        ]
    },
    {
        header: '"dimClient" - данные о клиенте',
        lines: [
            ['keyClient', 'int not null', 'Суррогатный первичный ключ со свойством IDENTITY(1, 1)',],
            ['altKeyClient', 'int not null', 'Бизнес ключ клиента из транзакционной бд',],
            ['Sex', 'varchar not null', 'Пол клиента',],
        ]
    },
    {
        header: '"miniDimClientInfo" - часто изменяющиеся данные таблицы dimClient',
        lines: [
            ['keyClientInfo', 'int not null', 'Суррогатный первичный ключ со свойством IDENTITY(1, 1)',],
            ['Age', 'int null', 'Возвраст клиента',],
            ['KindOfActivity', 'varchar null', 'Профессия клиента',],
            ['Married', 'bool null', 'Состоит ли клиент в браке',],
        ]
    },
]

const outLines = [];

inputData.forEach(data => {
    outLines.push(data.header);

    const maxColumnLen = data.lines.concat(headers).reduce((acc, cur) => {
        for (const i in acc) {
            acc[i] = Math.max(acc[i], cur[i]?.length ?? 0);
        }
        return acc;
    }, Array.from(data.lines[0]).map(_ => 0));

    const tableBorders = maxColumnLen.map(cnt => '-'.repeat(cnt)).join('-+-');
    const headerText = headers.map((value, index) => addSpacesMiddle(value, maxColumnLen[index], true)).join(' | ');

    outLines.push(`${tableBorder.start.border}${tableBorders}${tableBorder.end.border}`);
    outLines.push(`${tableBorder.start.line}${headerText}${tableBorder.end.line}`);
    outLines.push(`${tableBorder.start.border}${tableBorders}${tableBorder.end.border}`);

    data.lines.forEach(line => {
        // const linetext = line.map((value, index) => addSpacesLeft(value, maxColumnLen[index])).join(' | ');
        const linetext = [addSpacesLeft(line[0], maxColumnLen[0]), addSpacesMiddle(line[1], maxColumnLen[1]), addSpacesRight(line[2], maxColumnLen[2]), ].join(' | ');
        outLines.push(`${tableBorder.start.line}${linetext}${tableBorder.end.line}`);
    });

    outLines.push(`${tableBorder.start.border}${tableBorders}${tableBorder.end.border}`);
});

console.log(outLines.join('\n'));

function addSpacesMiddle(text, targetLen) {
    const freeSpaceLen = (targetLen - text.length) / 2;
    const beforeLen = Math.floor(freeSpaceLen);
    const beforeText = beforeLen > 0 ? ' '.repeat(beforeLen) : '';
    const afterLen = Math.ceil(freeSpaceLen);
    const afterText = afterLen > 0 ? ' '.repeat(afterLen) : '';
    return `${beforeText}${text}${afterText}`;
}

function addSpacesRight(text, targetLen) {
    const freeSpaceLen = targetLen - text.length;
    const afterText = freeSpaceLen > 0 ? ' '.repeat(freeSpaceLen) : '';
    return `${text}${afterText}`;
}

function addSpacesLeft(text, targetLen) {
    const freeSpaceLen = targetLen - text.length;
    const beforeText = freeSpaceLen > 0 ? ' '.repeat(freeSpaceLen) : '';
    return `${beforeText}${text}`;
}

