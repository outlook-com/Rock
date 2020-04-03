<template>
    <div class="panel panel-block obsidian-block">
        <div class="panel-heading">
            <h1 class="panel-title">
                <i v-if="iconCssClass" :class="iconCssClass"></i>
                {{title}}
            </h1>
        </div>
        <div class="panel-body">
            <slot />
        </div>
    </div>
</template>

<script>
    module.exports = {
        name: 'rock-block',
        props: {
            title: {
                type: String,
                required: true
            },
            iconCssClass: {
                type: String,
                default: ''
            }
        }
    };
</script>
