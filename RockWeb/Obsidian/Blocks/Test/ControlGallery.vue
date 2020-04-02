<template>
    <rock-block title="Obsidian Control Gallery" icon-css-class="fa fa-flask">
        <rock-definedtypepicker label="Defined Type" v-model="definedTypeGuid" />
        <rock-definedvaluepicker label="Defined Value" v-model="definedValueGuid" :defined-type-guid="definedTypeGuid" />
    </rock-block>
</template>

<script>
    export default {
        name: 'Test_ControlGallery',
        data() {
            return {
                definedTypeGuid: '',
                definedValueGuid: ''
            };
        }
    };
</script>