<template>
    <rock-block title="Obsidian Control Gallery" icon-css-class="fa fa-flask">
        <rock-definedtypepicker label="Defined Type" v-model="definedTypeGuid"></rock-definedtypepicker>
        <rock-definedvaluepicker label="Defined Value" v-model="definedValueGuid" :defined-type-guid="definedTypeGuid"></rock-definedvaluepicker>
    </rock-block>
</template>

<script>
    module.exports = {
        name: 'Test_ControlGallery',
        components: {
            'rock-block': Obsidian.Elements.Block,
            'rock-definedtypepicker': Obsidian.Controls.DefinedTypePicker,
            'rock-definedvaluepicker': Obsidian.Controls.DefinedValuePicker 
        },
        data() {
            return {
                definedTypeGuid: '',
                definedValueGuid: ''
            };
        }
    };
</script>